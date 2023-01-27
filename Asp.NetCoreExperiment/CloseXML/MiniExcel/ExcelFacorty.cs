namespace MiniExcelLibs
{
    using MiniExcelLibs.OpenXml;
    using System;
    using MiniExcelLibs.Csv;
    using System.IO;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Data;
    using MiniExcelLibs.Utils;

    internal class ExcelReaderFactory
    {
        internal static IExcelReader GetProvider(Stream stream, ExcelType excelType, IConfiguration configuration)
        {
            switch (excelType)
            {
                case ExcelType.CSV:
                    return new CsvReader(stream, configuration);
                case ExcelType.XLSX:
                    return new ExcelOpenXmlSheetReader(stream, configuration);
                default:
                    throw new NotSupportedException($"Please Issue for me");
            }
        }
    }

    internal class ExcelWriterFactory
    {
        internal static IExcelWriter GetProvider(Stream stream, object value, string sheetName, ExcelType excelType, IConfiguration configuration, bool printHeader)
        {
            if (string.IsNullOrEmpty(sheetName))
                throw new InvalidDataException("Sheet name can not be empty or null");
            if (excelType == ExcelType.UNKNOWN)
                throw new InvalidDataException("Please specify excelType");

            switch (excelType)
            {
                case ExcelType.CSV:
                    return new CsvWriter(stream, value, configuration, printHeader);
                case ExcelType.XLSX:
                    return new ExcelOpenXmlSheetWriter(stream, value, sheetName, configuration, printHeader);
                default:
                    throw new NotSupportedException($"Please Issue for me");
            }
        }
        internal static IExcelWriter GetProvider(Stream stream, object value, string sheetName, ExcelType excelType, IConfiguration configuration, bool printHeader, Func<IDataReader,  List<Tuple<int,string, object>>> func = null)
        {
            if (string.IsNullOrEmpty(sheetName))
                throw new InvalidDataException("Sheet name can not be empty or null");
            if (excelType == ExcelType.UNKNOWN)
                throw new InvalidDataException("Please specify excelType");

            switch (excelType)
            {
                case ExcelType.CSV:
                    return new CsvWriter(stream, value, configuration, printHeader);
                case ExcelType.XLSX:
                    return new ExcelOpenXmlSheetWriter(stream, value, sheetName, configuration, printHeader, func);
                default:
                    throw new NotSupportedException($"Please Issue for me");
            }
        }
        //internal static IExcelWriter GetProvider(Stream stream, object value, string sheetName, ExcelType excelType, IConfiguration configuration, bool printHeader, Dictionary<string, KeyValuePair<string, string>> replaceDictionary)
        //{
        //    if (string.IsNullOrEmpty(sheetName))
        //        throw new InvalidDataException("Sheet name can not be empty or null");
        //    if (excelType == ExcelType.UNKNOWN)
        //        throw new InvalidDataException("Please specify excelType");

        //    switch (excelType)
        //    {
        //        case ExcelType.CSV:
        //            return new CsvWriter(stream, value, configuration, printHeader);
        //        case ExcelType.XLSX:
        //            return new ExcelOpenXmlSheetWriter(stream, value, sheetName, configuration, printHeader, replaceDictionary);
        //        default:
        //            throw new NotSupportedException($"Please Issue for me");
        //    }
        //}
    }

    internal class ExcelTemplateFactory
    {
        internal static IExcelTemplateAsync GetProvider(Stream stream, IConfiguration configuration, ExcelType excelType = ExcelType.XLSX)
        {
            switch (excelType)
            {
                case ExcelType.XLSX:
                    return new ExcelOpenXmlTemplate(stream, configuration);
                default:
                    throw new NotSupportedException($"Please Issue for me");
            }
        }
    }
}
