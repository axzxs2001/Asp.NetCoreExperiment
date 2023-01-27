﻿using MiniExcelLibs.Utils;
using MiniExcelLibs.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MiniExcelLibs.Utils.ImageHelper;
using static System.Collections.Specialized.BitVector32;

namespace MiniExcelLibs.OpenXml
{
    internal class FileDto
    {
        public string ID { get; set; } = $"R{Guid.NewGuid():N}";
        public string Extension { get; set; }
        public string Path { get { return $"xl/media/{ID}.{Extension}"; } }
        public string Path2 { get { return $"/xl/media/{ID}.{Extension}"; } }
        public Byte[] Byte { get; set; }
        public int RowIndex { get; set; }
        public int CellIndex { get; set; }
        public bool IsImage { get; set; } = false;
        public int SheetId { get; set; }
    }
    internal class SheetDto
    {
        public string ID { get; set; } = $"R{Guid.NewGuid():N}";
        public string Name { get; set; }
        public int SheetIdx { get; set; }
        public string Path { get { return $"xl/worksheets/sheet{SheetIdx}.xml"; } }
    }
    internal class DrawingDto
    {
        public string ID { get; set; } = $"R{Guid.NewGuid():N}";
    }
    internal partial class ExcelOpenXmlSheetWriter : IExcelWriter
    {
        private readonly MiniExcelZipArchive _archive;
        private readonly static UTF8Encoding _utf8WithBom = new System.Text.UTF8Encoding(true);
        private readonly OpenXmlConfiguration _configuration;
        private readonly Stream _stream;
        private readonly bool _printHeader;
        private readonly object _value;
        private readonly List<SheetDto> _sheets = new List<SheetDto>();
        private readonly List<FileDto> _files = new List<FileDto>();
        private int currentSheetIndex = 0;      
        private readonly Func<IDataReader, List<Tuple<int,string, object>>> _func;   

        public ExcelOpenXmlSheetWriter(Stream stream, object value, string sheetName, IConfiguration configuration, bool printHeader)
        {       
            this._stream = stream;
            // Why ZipArchiveMode.Update not ZipArchiveMode.Create?
            // R : Mode create - ZipArchiveEntry does not support seeking.'
            this._configuration = configuration as OpenXmlConfiguration ?? OpenXmlConfiguration.DefaultConfig;
            if (_configuration.FastMode)
                this._archive = new MiniExcelZipArchive(_stream, ZipArchiveMode.Update, true, _utf8WithBom);
            else
                this._archive = new MiniExcelZipArchive(_stream, ZipArchiveMode.Create, true, _utf8WithBom);
            this._printHeader = printHeader;
            this._value = value;
            _sheets.Add(new SheetDto { Name = sheetName, SheetIdx = 1 }); //TODO:remove

        }
        public ExcelOpenXmlSheetWriter(Stream stream, object value, string sheetName, IConfiguration configuration, bool printHeader, Func<IDataReader, List<Tuple<int,string, object>>> func)
        {
            _func = func;
            this._stream = stream;
            // Why ZipArchiveMode.Update not ZipArchiveMode.Create?
            // R : Mode create - ZipArchiveEntry does not support seeking.'
            this._configuration = configuration as OpenXmlConfiguration ?? OpenXmlConfiguration.DefaultConfig;
            if (_configuration.FastMode)
                this._archive = new MiniExcelZipArchive(_stream, ZipArchiveMode.Update, true, _utf8WithBom);
            else
                this._archive = new MiniExcelZipArchive(_stream, ZipArchiveMode.Create, true, _utf8WithBom);
            this._printHeader = printHeader;
            this._value = value;
            _sheets.Add(new SheetDto { Name = sheetName, SheetIdx = 1 }); //TODO:remove

        }      
        public ExcelOpenXmlSheetWriter()
        {
        }

        public void SaveAs()
        {
            GenerateDefaultOpenXml();
            {
                if (_value is IDictionary<string, object>)
                {
                    var sheetId = 0;
                    var sheets = _value as IDictionary<string, object>;
                    _sheets.RemoveAt(0);//TODO:remove
                    foreach (var sheet in sheets)
                    {
                        sheetId++;
                        var s = new SheetDto { Name = sheet.Key, SheetIdx = sheetId };
                        _sheets.Add(s); //TODO:remove

                        currentSheetIndex = sheetId;

                        CreateSheetXml(sheet.Value, s.Path);
                    }
                }
                else if (_value is DataSet)
                {
                    var sheetId = 0;
                    var sheets = _value as DataSet;
                    _sheets.RemoveAt(0);//TODO:remove
                    foreach (DataTable dt in sheets.Tables)
                    {
                        sheetId++;
                        var s = new SheetDto { Name = dt.TableName, SheetIdx = sheetId };
                        _sheets.Add(s); //TODO:remove
                        var sheetPath = s.Path;

                        currentSheetIndex = sheetId;

                        CreateSheetXml(dt, sheetPath);
                    }
                }
                else
                {
                    //Single sheet export.
                    currentSheetIndex++;

                    CreateSheetXml(_value, _sheets[0].Path);
                }
            }
            GenerateEndXml();
            _archive.Dispose();
        }

        private void CreateSheetXml(object value, string sheetPath)
        {
            ZipArchiveEntry entry = _archive.CreateEntry(sheetPath, CompressionLevel.Fastest);
            using (var zipStream = entry.Open())
            using (MiniExcelStreamWriter writer = new MiniExcelStreamWriter(zipStream, _utf8WithBom, _configuration.BufferSize))
            {
                if (value == null)
                {
                    WriteEmptySheet(writer);
                    goto End; //for re-using code
                }

                var type = value.GetType();

                Type genericType = null;

                //DapperRow

                if (value is IDataReader)
                {
                    GenerateSheetByIDataReader(writer, value as IDataReader);
                }
                else if (value is IEnumerable)
                {
                    var values = value as IEnumerable;

                    // try to get type from reflection
                    // genericType = null

                    var rowCount = 0;

                    var maxColumnIndex = 0;
                    //List<object> keys = new List<object>();
                    List<ExcelColumnInfo> props = null;
                    string mode = null;

                    // reason : https://stackoverflow.com/questions/66797421/how-replace-top-format-mark-after-MiniExcelStreamWriter-writing
                    // check mode & get maxRowCount & maxColumnIndex
                    {
                        foreach (var item in values) //TODO: need to optimize
                        {
                            rowCount = checked(rowCount + 1);

                            //TODO: if item is null but it's collection<T>, it can get T type from reflection
                            if (item != null && mode == null)
                            {
                                if (item is IDictionary<string, object>)
                                {
                                    mode = "IDictionary<string, object>";
                                    var dic = item as IDictionary<string, object>;
                                    props = GetDictionaryColumnInfo(dic, null);
                                    maxColumnIndex = props.Count;
                                }
                                else if (item is IDictionary)
                                {
                                    var dic = item as IDictionary;
                                    mode = "IDictionary";
                                    props = GetDictionaryColumnInfo(null, dic);
                                    //maxColumnIndex = dic.Keys.Count; 
                                    maxColumnIndex = props.Count; // why not using keys, because ignore attribute ![image](https://user-images.githubusercontent.com/12729184/163686902-286abb70-877b-4e84-bd3b-001ad339a84a.png)
                                }
                                else
                                {
                                    var _t = item.GetType();
                                    if (_t != genericType)
                                        genericType = item.GetType();
                                    genericType = item.GetType();
                                    SetGenericTypePropertiesMode(genericType, ref mode, out maxColumnIndex, out props);
                                }

                                var collection = value as ICollection;
                                if (collection != null)
                                {
                                    rowCount = checked((value as ICollection).Count);
                                    break;
                                }
                                continue;
                            }
                        }
                    }

                    if (rowCount == 0)
                    {
                        // only when empty IEnumerable need to check this issue #133  https://github.com/shps951023/MiniExcel/issues/133
                        genericType = TypeHelper.GetGenericIEnumerables(values).FirstOrDefault();
                        if (genericType == null || genericType == typeof(object) // sometime generic type will be object, e.g: https://user-images.githubusercontent.com/12729184/132812859-52984314-44d1-4ee8-9487-2d1da159f1f0.png
                            || typeof(IDictionary<string, object>).IsAssignableFrom(genericType)
                            || typeof(IDictionary).IsAssignableFrom(genericType))
                        {
                            WriteEmptySheet(writer);
                            goto End; //for re-using code
                        }
                        else
                        {
                            SetGenericTypePropertiesMode(genericType, ref mode, out maxColumnIndex, out props);
                        }
                    }

                    writer.Write($@"<?xml version=""1.0"" encoding=""utf-8""?><x:worksheet xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"" xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" >");

                    // dimension 
                    var maxRowIndex = rowCount + (_printHeader && rowCount > 0 ? 1 : 0);  //TODO:it can optimize
                    writer.Write($@"<x:dimension ref=""{GetDimensionRef(maxRowIndex, maxColumnIndex)}""/>");

                    //cols:width
                    var ecwProp = props?.Where(x => x?.ExcelColumnWidth != null).ToList();
                    if (ecwProp != null && ecwProp.Count > 0)
                    {
                        writer.Write($@"<x:cols>");
                        foreach (var p in ecwProp)
                        {
                            writer.Write($@"<x:col min=""{p.ExcelColumnIndex + 1}"" max=""{p.ExcelColumnIndex + 1}"" width=""{p.ExcelColumnWidth}"" customWidth=""1"" />");
                        }
                        writer.Write($@"</x:cols>");
                    }

                    //header
                    writer.Write($@"<x:sheetData>");
                    var yIndex = 1;
                    var xIndex = 1;
                    if (_printHeader)
                    {
                        var cellIndex = xIndex;
                        writer.Write($"<x:row r=\"{yIndex}\">");

                        foreach (var p in props)
                        {
                            if (p == null)
                            {
                                cellIndex++; //reason : https://github.com/shps951023/MiniExcel/issues/142
                                continue;
                            }

                            var r = ExcelOpenXmlUtils.ConvertXyToCell(cellIndex, yIndex);
                            WriteC(writer, r, columnName: p.ExcelColumnName);
                            cellIndex++;
                        }

                        writer.Write($"</x:row>");
                        yIndex++;
                    }

                    // body
                    if (mode == "IDictionary<string, object>") //Dapper Row
                        GenerateSheetByColumnInfo<IDictionary<string, object>>(writer, value as IEnumerable, props, xIndex, yIndex);
                    else if (mode == "IDictionary") //IDictionary
                        GenerateSheetByColumnInfo<IDictionary>(writer, value as IEnumerable, props, xIndex, yIndex);
                    else if (mode == "Properties")
                        GenerateSheetByColumnInfo<object>(writer, value as IEnumerable, props, xIndex, yIndex);
                    else
                        throw new NotImplementedException($"Type {type.Name} & genericType {genericType.Name} not Implemented. please issue for me.");
                    writer.Write("</x:sheetData>");
                    if (_configuration.AutoFilter)
                        writer.Write($"<x:autoFilter ref=\"A1:{ExcelOpenXmlUtils.ConvertXyToCell(maxColumnIndex, maxRowIndex == 0 ? 1 : maxRowIndex)}\" />");
                    writer.Write("<x:drawing  r:id=\"drawing" + currentSheetIndex + "\" /></x:worksheet>");
                }
                else if (value is DataTable)
                {
                    GenerateSheetByDataTable(writer, value as DataTable);
                }
                else
                {
                    throw new NotImplementedException($"Type {type.Name} & genericType {genericType.Name} not Implemented. please issue for me.");
                }
            }
        End: //for re-using code
            _zipDictionary.Add(sheetPath, new ZipPackageInfo(entry, "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"));
        }

        private List<ExcelColumnInfo> GetDictionaryColumnInfo(IDictionary<string, object> dicString, IDictionary dic)
        {
            List<ExcelColumnInfo> props;
            var _props = new List<ExcelColumnInfo>();
            if (dicString != null)
                foreach (var key in dicString.Keys)
                    SetDictionaryColumnInfo(_props, key);
            else if (dic != null)
                foreach (var key in dic.Keys)
                    SetDictionaryColumnInfo(_props, key);
            else
                throw new NotSupportedException("SetDictionaryColumnInfo Error");
            props = CustomPropertyHelper.SortCustomProps(_props);
            return props;
        }

        private void SetDictionaryColumnInfo(List<ExcelColumnInfo> _props, object key)
        {
            var p = new ExcelColumnInfo();
            p.ExcelColumnName = key?.ToString();
            p.Key = key;
            // TODO:Dictionary value type is not fiexed
            //var _t = 
            //var gt = Nullable.GetUnderlyingType(p.PropertyType);
            var isIgnore = false;
            if (_configuration.DynamicColumns != null && _configuration.DynamicColumns.Length > 0)
            {
                var dynamicColumn = _configuration.DynamicColumns.SingleOrDefault(_ => _.Key == key.ToString());
                if (dynamicColumn != null)
                {
                    p.Nullable = true;
                    //p.ExcludeNullableType = item2[key]?.GetType();
                    if (dynamicColumn.Format != null)
                        p.ExcelFormat = dynamicColumn.Format;
                    if (dynamicColumn.Aliases != null)
                        p.ExcelColumnAliases = dynamicColumn.Aliases;
                    if (dynamicColumn.IndexName != null)
                        p.ExcelIndexName = dynamicColumn.IndexName;
                    p.ExcelColumnIndex = dynamicColumn.Index;
                    if (dynamicColumn.Name != null)
                        p.ExcelColumnName = dynamicColumn.Name;
                    isIgnore = dynamicColumn.Ignore;
                    p.ExcelColumnWidth = dynamicColumn.Width;
                }
            }
            if (!isIgnore)
                _props.Add(p);
        }

        private void SetGenericTypePropertiesMode(Type genericType, ref string mode, out int maxColumnIndex, out List<ExcelColumnInfo> props)
        {
            mode = "Properties";
            if (genericType.IsValueType)
                throw new NotImplementedException($"MiniExcel not support only {genericType.Name} value generic type");
            else if (genericType == typeof(string) || genericType == typeof(DateTime) || genericType == typeof(Guid))
                throw new NotImplementedException($"MiniExcel not support only {genericType.Name} generic type");
            props = CustomPropertyHelper.GetSaveAsProperties(genericType, _configuration);

            maxColumnIndex = props.Count;
        }

        private void WriteEmptySheet(MiniExcelStreamWriter writer)
        {
            writer.Write($@"<?xml version=""1.0"" encoding=""utf-8""?><x:worksheet xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main""><x:dimension ref=""A1""/><x:sheetData></x:sheetData></x:worksheet>");
        }
        private void GenerateSheetByColumnInfo<T>(MiniExcelStreamWriter writer, IEnumerable value, List<ExcelColumnInfo> props, int xIndex = 1, int yIndex = 1)
        {
            var isDic = typeof(T) == typeof(IDictionary);
            var isDapperRow = typeof(T) == typeof(IDictionary<string, object>);
            foreach (T v in value)
            {
                writer.Write($"<x:row r=\"{yIndex}\">");
                var cellIndex = xIndex;
                foreach (var p in props)
                {
                    if (p == null) //reason:https://github.com/shps951023/MiniExcel/issues/142
                    {
                        cellIndex++;
                        continue;
                    }
                    object cellValue = null;
                    if (isDic)
                    {
                        cellValue = ((IDictionary)v)[p.Key];
                        //WriteCell(writer, yIndex, cellIndex, cellValue, null); // why null because dictionary that needs to check type every time
                        //TODO: user can specefic type to optimize efficiency
                    }
                    else if (isDapperRow)
                    {
                        cellValue = ((IDictionary<string, object>)v)[p.Key.ToString()];
                    }
                    else
                    {
                        cellValue = p.Property.GetValue(v);
                    }
                    WriteCell(writer, yIndex, cellIndex, cellValue, p);


                    cellIndex++;
                }
                writer.Write($"</x:row>");
                yIndex++;
            }
        }

        private void WriteCell(MiniExcelStreamWriter writer, int rowIndex, int cellIndex, object value, ExcelColumnInfo p=null,bool isMoney=false)
        {
            var v = string.Empty;
            var t = "str";
            var s = "2";
            if (value == null)
            {
                v = "";
            }
            else if (value is string str)
            {
                v = ExcelOpenXmlUtils.EncodeXML(str);
            }
            else if (p?.ExcelFormat != null && value is IFormattable formattableValue)
            {
                var formattedStr = formattableValue.ToString(p.ExcelFormat, _configuration.Culture);
                v = ExcelOpenXmlUtils.EncodeXML(formattedStr);
            }
            else
            {
                Type type = null;
                if (p == null || p.Key != null)
                {
                    // TODO: need to optimize 
                    // Dictionary need to check type every time, so it's slow..
                    type = value.GetType();
                    type = Nullable.GetUnderlyingType(type) ?? type;
                }
                else
                {
                    type = p.ExcludeNullableType; //sometime it doesn't need to re-get type like prop
                }

                if (type.IsEnum)
                {
                    t = "str";
                    var description = CustomPropertyHelper.DescriptionAttr(type, value); //TODO: need to optimze
                    if (description != null)
                        v = description;
                    else
                        v = value.ToString();
                }
                else if (TypeHelper.IsNumericType(type))
                {
                    if (_configuration.Culture != CultureInfo.InvariantCulture)
                        t = "str"; //TODO: add style format
                    else
                        t = "n";

                    if (isMoney)
                    {
                        s = "5";
                    }

                    if (type.IsAssignableFrom(typeof(decimal)))
                        v = ((decimal)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(Int32)))
                        v = ((Int32)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(Double)))
                        v = ((Double)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(Int64)))
                        v = ((Int64)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(UInt32)))
                        v = ((UInt32)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(UInt16)))
                        v = ((UInt16)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(UInt64)))
                        v = ((UInt64)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(Int16)))
                        v = ((Int16)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(Single)))
                        v = ((Single)value).ToString(_configuration.Culture);
                    else if (type.IsAssignableFrom(typeof(Single)))
                        v = ((Single)value).ToString(_configuration.Culture);
                    else
                        v = (decimal.Parse(value.ToString())).ToString(_configuration.Culture);
                }
                else if (type == typeof(bool))
                {
                    t = "b";
                    v = (bool)value ? "1" : "0";
                }
                else if (type == typeof(byte[]) && _configuration.EnableConvertByteArray)
                {
                    var bytes = (byte[])value;
                    if (bytes != null)
                    {
                        // TODO: Setting configuration because it might have high cost?
                        var format = ImageHelper.GetImageFormat(bytes);
                        //it can't insert to zip first to avoid cache image to memory
                        //because sheet xml is opening.. https://github.com/shps951023/MiniExcel/issues/304#issuecomment-1017031691
                        //int rowIndex, int cellIndex
                        var file = new FileDto()
                        {
                            Byte = bytes,
                            RowIndex = rowIndex,
                            CellIndex = cellIndex,
                            SheetId = currentSheetIndex
                        };
                        if (format != ImageFormat.unknown)
                        {
                            file.Extension = format.ToString();
                            file.IsImage = true;
                        }
                        else
                        {
                            file.Extension = "bin";
                        }
                        _files.Add(file);

                        //TODO:Convert to base64
                        var base64 = $"@@@fileid@@@,{file.Path}";
                        v = ExcelOpenXmlUtils.EncodeXML(base64);
                        s = "4";
                    }
                }
                else if (type == typeof(DateTime))
                {
                    if (_configuration.Culture != CultureInfo.InvariantCulture)
                    {
                        t = "str";
                        v = ((DateTime)value).ToString(_configuration.Culture);
                    }
                    else if (p == null || p.ExcelFormat == null)
                    {
                        t = null;
                        s = "3";
                        v = ((DateTime)value).ToOADate().ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        // TODO: now it'll lose date type information
                        t = "str";
                        v = ((DateTime)value).ToString(p.ExcelFormat, _configuration.Culture);
                    }
                }
                else
                {
                    //TODO: _configuration.Culture
                    v = ExcelOpenXmlUtils.EncodeXML(value.ToString());
                }
            }

            var columname = ExcelOpenXmlUtils.ConvertXyToCell(cellIndex, rowIndex);
            if (v != null && (v.StartsWith(" ", StringComparison.Ordinal) || v.EndsWith(" ", StringComparison.Ordinal))) /*Prefix and suffix blank space will lost after SaveAs #294*/
                writer.Write($"<x:c r=\"{columname}\" {(t == null ? "" : $"t =\"{t}\"")} s=\"{s}\" xml:space=\"preserve\"><x:v>{v}</x:v></x:c>");
            else
                //t check avoid format error ![image](https://user-images.githubusercontent.com/12729184/118770190-9eee3480-b8b3-11eb-9f5a-87a439f5e320.png)
                writer.Write($"<x:c r=\"{columname}\" {(t == null ? "" : $"t =\"{t}\"")} s=\"{s}\"><x:v>{v}</x:v></x:c>");
        }

        private void GenerateSheetByDataTable(MiniExcelStreamWriter writer, DataTable value)
        {
            var xy = ExcelOpenXmlUtils.ConvertCellToXY("A1");

            //GOTO Top Write:
            writer.Write($@"<?xml version=""1.0"" encoding=""utf-8""?><x:worksheet xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">");
            {
                var yIndex = xy.Item2;

                // dimension
                var maxRowIndex = value.Rows.Count + (_printHeader && value.Rows.Count > 0 ? 1 : 0);
                var maxColumnIndex = value.Columns.Count;
                writer.Write($@"<x:dimension ref=""{GetDimensionRef(maxRowIndex, maxColumnIndex)}""/><x:sheetData>");

                if (_printHeader)
                {
                    writer.Write($"<x:row r=\"{yIndex}\">");
                    var xIndex = xy.Item1;
                    foreach (DataColumn c in value.Columns)
                    {
                        var r = ExcelOpenXmlUtils.ConvertXyToCell(xIndex, yIndex);
                        WriteC(writer, r, columnName: c.Caption ?? c.ColumnName);
                        xIndex++;
                    }
                    writer.Write($"</x:row>");
                    yIndex++;
                }

                for (int i = 0; i < value.Rows.Count; i++)
                {
                    writer.Write($"<x:row r=\"{yIndex}\">");
                    var xIndex = xy.Item1;

                    for (int j = 0; j < value.Columns.Count; j++)
                    {
                        var cellValue = value.Rows[i][j];
                        WriteCell(writer, yIndex, xIndex, cellValue, null);
                        xIndex++;
                    }
                    writer.Write($"</x:row>");
                    yIndex++;
                }
            }
            writer.Write("</x:sheetData></x:worksheet>");
        }
        private void GenerateSheetByIDataReader(MiniExcelStreamWriter writer, IDataReader reader)
        {
            var xy = ExcelOpenXmlUtils.ConvertCellToXY("A1"); /*TODO:code smell*/
            long dimensionWritePosition = 0;
            writer.Write($@"<?xml version=""1.0"" encoding=""utf-8""?><x:worksheet xmlns:x=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">");
            var yIndex = xy.Item2;
            var xIndex = 0;
            {

                if (_configuration.FastMode)
                {
                     dimensionWritePosition = writer.WriteAndFlush($@"<x:dimension ref=""");
                    writer.Write("                              />"); // end of code will be replaced
                }

                int fieldCount = reader.FieldCount;
                //处理列宽度
                if (_printHeader)
                {
                    writer.Write($@"<x:cols>");
                    for (int i = 0; i < fieldCount; i++)
                    {      
                        var columnName = reader.GetName(i);
                        var width = _configuration.DynamicColumns.FirstOrDefault(s => s.Key.ToLower() == columnName)?.Width;
                        writer.Write($@"<x:col min=""{i+ 1}"" max=""{i + 1}"" {(width.HasValue?$@"width=""{width.Value}""":"")} customWidth=""1"" />");
                    }
                    writer.Write($@"</x:cols>");
                }
                //处理数据
                writer.Write("<x:sheetData>");
                //处理表头
                if (_printHeader)
                {
                    writer.Write($"<x:row r=\"{yIndex}\">");
                    xIndex = xy.Item1;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        var r = ExcelOpenXmlUtils.ConvertXyToCell(xIndex, yIndex);
                        var columnName = reader.GetName(i);
                        columnName = _configuration.DynamicColumns.FirstOrDefault(s => s.Key.ToLower() == columnName.ToLower())?.Name;
                        WriteC(writer, r, columnName);
                        xIndex++;
                    }
                    writer.Write($"</x:row>");
                    yIndex++;
                }
                //处理数据
                while (reader.Read())
                {
                    writer.Write($"<x:row r=\"{yIndex}\">");
                    //调用处理数据的接口
                    var list = _func(reader);
                    foreach (var item in list)
                    {
                       var format= _configuration.DynamicColumns.FirstOrDefault(s => s.Key.ToLower() == item.Item2.ToLower())?.Format;
                       if(!string.IsNullOrWhiteSpace(format)&& item.Item3 is DateTime)
                        {                       
                            WriteCell(writer, yIndex, item.Item1, Convert.ToDateTime(item.Item3).ToString(format));
                        }
                        else
                        {
                            WriteCell(writer, yIndex, item.Item1, item.Item3, null,format== "Currency");
                        }
                    }
                    writer.Write($"</x:row>");
                    yIndex++;
                }
            }
            writer.Write("</x:sheetData>");
            if (_configuration.AutoFilter)
                writer.Write($"<x:autoFilter ref=\"A1:{ExcelOpenXmlUtils.ConvertXyToCell((xIndex - 1)/*TODO:code smell*/, yIndex - 1)}\" />");
            writer.WriteAndFlush("</x:worksheet>");

            if (_configuration.FastMode)
            {
                writer.SetPosition(dimensionWritePosition);
                writer.WriteAndFlush($@"A1:{ExcelOpenXmlUtils.ConvertXyToCell((xIndex - 1)/*TODO:code smell*/, yIndex - 1)}""");
            }
        }



        private static void WriteC(MiniExcelStreamWriter writer, string r, string columnName)
        {
            writer.Write($"<x:c r=\"{r}\" t=\"str\" s=\"1\">");
            writer.Write($"<x:v>{ExcelOpenXmlUtils.EncodeXML(columnName)}"); //issue I45TF5
            writer.Write($"</x:v>");
            writer.Write($"</x:c>");
        }

        private void GenerateEndXml()
        {
            //Files
            {
                foreach (var item in _files)
                {
                    this.CreateZipEntry(item.Path, item.Byte);
                }
            }

            // styles.xml 
            {
                var styleXml = string.Empty;

                if (_configuration.TableStyles == TableStyles.None)
                {
                    styleXml = _noneStylesXml;
                }
                else if (_configuration.TableStyles == TableStyles.Default)
                {
                    styleXml = _defaultStylesXml;
                }
                CreateZipEntry(@"xl/styles.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml", styleXml);
            }

            // drawing rel
            {
                for (int j = 0; j < _sheets.Count; j++)
                {
                    var drawing = new StringBuilder();
                    foreach (var i in _files.Where(w => w.IsImage && w.SheetId == j + 1))
                    {
                        drawing.AppendLine($@"<Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/image"" Target=""{i.Path2}"" Id=""{i.ID}"" />");
                    }
                    CreateZipEntry($"xl/drawings/_rels/drawing{j + 1}.xml.rels", "",
                        _defaultDrawingXmlRels.Replace("{{format}}", drawing.ToString()));
                }

            }
            // drawing
            {
                for (int j = 0; j < _sheets.Count; j++)
                {
                    var drawing = new StringBuilder();
                    foreach (var i in _files.Where(w => w.IsImage && w.SheetId == j + 1))
                    {
                        drawing.Append($@"<xdr:oneCellAnchor>
        <xdr:from>
            <xdr:col>{i.CellIndex - 1/* why -1 : https://user-images.githubusercontent.com/12729184/150460189-f08ed939-44d4-44e1-be6e-9c533ece6be8.png*/}</xdr:col>
            <xdr:colOff>0</xdr:colOff>
            <xdr:row>{i.RowIndex - 1}</xdr:row>
            <xdr:rowOff>0</xdr:rowOff>
        </xdr:from>
        <xdr:ext cx=""609600"" cy=""190500"" />
        <xdr:pic>
            <xdr:nvPicPr>
                <xdr:cNvPr id=""{_files.IndexOf(i) + 1}"" descr="""" name=""2a3f9147-58ea-4a79-87da-7d6114c4877b"" />
                <xdr:cNvPicPr>
                    <a:picLocks noChangeAspect=""1"" />
                </xdr:cNvPicPr>
            </xdr:nvPicPr>
            <xdr:blipFill>
                <a:blip r:embed=""{i.ID}"" cstate=""print"" />
                <a:stretch>
                    <a:fillRect />
                </a:stretch>
            </xdr:blipFill>
            <xdr:spPr>
                <a:xfrm>
                    <a:off x=""0"" y=""0"" />
                    <a:ext cx=""0"" cy=""0"" />
                </a:xfrm>
                <a:prstGeom prst=""rect"">
                    <a:avLst />
                </a:prstGeom>
            </xdr:spPr>
        </xdr:pic>
        <xdr:clientData />
    </xdr:oneCellAnchor>");
                    }
                    CreateZipEntry($"xl/drawings/drawing{j + 1}.xml", "application/vnd.openxmlformats-officedocument.drawing+xml",
                        _defaultDrawing.Replace("{{format}}", drawing.ToString()));
                }
            }

            // workbook.xml 、 workbookRelsXml
            {
                var workbookXml = new StringBuilder();
                var workbookRelsXml = new StringBuilder();

                var sheetId = 0;
                foreach (var s in _sheets)
                {
                    sheetId++;
                    workbookXml.AppendLine($@"<x:sheet name=""{s.Name}"" sheetId=""{sheetId}"" r:id=""{s.ID}"" />");
                    workbookRelsXml.AppendLine($@"<Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""/{s.Path}"" Id=""{s.ID}"" />");

                    //TODO: support multiple drawing 
                    //TODO: ../drawings/drawing1.xml or /xl/drawings/drawing1.xml
                    var sheetRelsXml = $@"<Relationship Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing"" Target=""../drawings/drawing{sheetId}.xml"" Id=""drawing{sheetId}"" />";
                    CreateZipEntry($"xl/worksheets/_rels/sheet{s.SheetIdx}.xml.rels", "",
                        _defaultSheetRelXml.Replace("{{format}}", sheetRelsXml));
                }
                CreateZipEntry(@"xl/workbook.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml",
                    _defaultWorkbookXml.Replace("{{sheets}}", workbookXml.ToString()));
                CreateZipEntry(@"xl/_rels/workbook.xml.rels", "",
                    _defaultWorkbookXmlRels.Replace("{{sheets}}", workbookRelsXml.ToString()));
            }

            //[Content_Types].xml 
            {
                var sb = new StringBuilder(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?><Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types""><Default ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.printerSettings"" Extension=""bin""/><Default ContentType=""application/xml"" Extension=""xml""/><Default ContentType=""image/jpeg"" Extension=""jpg""/><Default ContentType=""image/png"" Extension=""png""/><Default ContentType=""image/gif"" Extension=""gif""/><Default ContentType=""application/vnd.openxmlformats-package.relationships+xml"" Extension=""rels""/>");
                foreach (var p in _zipDictionary)
                    sb.Append($"<Override ContentType=\"{p.Value.ContentType}\" PartName=\"/{p.Key}\" />");
                sb.Append("</Types>");
                ZipArchiveEntry entry = _archive.CreateEntry("[Content_Types].xml", CompressionLevel.Fastest);
                using (var zipStream = entry.Open())
                using (MiniExcelStreamWriter writer = new MiniExcelStreamWriter(zipStream, _utf8WithBom, _configuration.BufferSize))
                    writer.Write(sb.ToString());
            }
        }

        private string GetDimensionRef(int maxRowIndex, int maxColumnIndex)
        {
            string dimensionRef;
            if (maxRowIndex == 0 && maxColumnIndex == 0)
                dimensionRef = "A1";
            else if (maxColumnIndex == 1)
                dimensionRef = $"A{maxRowIndex}";
            else if (maxRowIndex == 0)
                dimensionRef = $"A1:{ColumnHelper.GetAlphabetColumnName(maxColumnIndex - 1)}1";
            else
                dimensionRef = $"A1:{ColumnHelper.GetAlphabetColumnName(maxColumnIndex - 1)}{maxRowIndex}";
            return dimensionRef;
        }

        public async Task SaveAsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => SaveAs(), cancellationToken).ConfigureAwait(false);
        }

        public void Insert()
        {
            throw new NotImplementedException();
        }
    }
}
