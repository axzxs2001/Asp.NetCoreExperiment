
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ArchitectureDemo04
{
    /// <summary>
    /// 报文类的父类
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// 组装发送报文格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var pros = this.GetType().GetProperties();
            var sortPro = new SortedList<int, PropertyInfo>();  
            foreach (var pro in pros)
            {
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    if (att is PackageAttribute)
                    {
                        var packageAtt = att as PackageAttribute;   
                        sortPro.Add(packageAtt.SN, pro);
                    }
                }
            }
            var content = new StringBuilder();
            #region 组合发送字符串
            //遍历属性 
            foreach (var pro in sortPro)
            {
                //遍历属性上的特性                
                foreach (var att in pro.Value.GetCustomAttributes(false))
                {
                    //判断是否为自定义的PackageAttribute类型
                    if (att is PackageAttribute)
                    {
                        //转换属性上的特性类
                        var packageAtt = att as PackageAttribute;
                        //取拼接时字符长度
                        var length = packageAtt.Length;
                        //取属性的值
                        var proValue = pro.Value.GetValue(this, new Object[0]);

                        //对decimal作处理
                        if (pro.Value.PropertyType.Name.ToLower() == "decimal")
                        {
                            proValue = Math.Round(Convert.ToDecimal(proValue), 2);
                            if (Encoding.Default.GetByteCount(proValue.ToString()) > length)
                            {
                                proValue = "0";
                            }
                        }
                        //判断字符串长度过长
                        if (proValue != null && (pro.Value.PropertyType.Name.ToLower() == "string"))
                        {
                            if (System.Text.Encoding.Default.GetBytes(proValue.ToString()).Length > length)
                            {
                                throw new Exception(string.Format("属性{0}的值{1},长度超过{2}", pro.Value.Name, proValue, length));
                            }
                        }
                        //如果值为非空
                        if (proValue != null)
                        {
                            //日期是右补空格，其他是左补空格
                            if (!packageAtt.IsDateTime)
                            {
                                //这里注册，有些属性是枚举类型，有些属性拼接枚举的值，有些取枚举值对应的枚举数值，这里是从该属性类型上的EnumValeuNumberAttribute特性的IsValue属性来判断的，IsValue为true，就取枚举的值，为false取该值对应的枚举数
                                if (pro.Value.PropertyType.IsEnum)
                                {
                                    foreach (var eatt in pro.Value.PropertyType.GetCustomAttributes(false))
                                    {
                                        if (eatt is EnumValeuNumberAttribute)
                                        {
                                            var enumVaNu = eatt as EnumValeuNumberAttribute;
                                            if (enumVaNu.IsChar)
                                            {
                                                var enumNumber = ((char)(int)Enum.Parse(pro.Value.PropertyType, proValue.ToString())).ToString();
                                                content.Append(enumNumber.ChineseCharacterLeft(length));
                                            }
                                            else
                                            {
                                                var enumNumber = ((int)Enum.Parse(pro.Value.PropertyType, proValue.ToString())).ToString();
                                                content.Append(enumNumber.ChineseCharacterLeft(length));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    content.Append(proValue.ToString().ChineseCharacterLeft(length));
                                }
                            }
                            else//日期类型右补空格
                            {
                                content.Append(proValue.ToString().ChineseCharacterRight(length));
                            }
                        }
                        else
                        {
                            content.Append("".ChineseCharacterLeft(length));
                        }
                    }
                }
            }
            #endregion
            return content.ToString();
        }


        /// <summary>
        /// 把一个字符串转成一个对象
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public  Entity ToEntity(Type entityType,string content)
        {
            var pros = entityType.GetProperties();
            //按照特性类上的SN序号把属性名存入集合proPackageList中
            List<PropertyInfo> proPackageList = new List<PropertyInfo>(pros.Length);
            //初始化属性集合

            for (int i = 0; i < pros.Length; i++)
            {
                foreach (var att in pros[i].GetCustomAttributes(false))
                {
                    if (att is PackageAttribute)
                    {
                        proPackageList.Add(null);
                        break;
                    }
                }
            }
            //按属性顺序排列属性
            foreach (var pro in pros)
            {
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    if (att is PackageAttribute)
                    {
                        var packageAtt = att as PackageAttribute;
                        var index = packageAtt.SN - 1;
                        proPackageList[index] = pro;
                    }
                }
            }

            //创建实体对象
            var constructor = entityType.GetConstructor(new Type[0]);
            var entity = constructor.Invoke(new object[0]);

            foreach (var pro in proPackageList)
            {
                //遍历属性上的特性
                foreach (var att in pro.GetCustomAttributes(false))
                {
                    //判断是否为自定义的PackageAttribute类型
                    if (att is PackageAttribute)
                    {
                        //转换属性上的特性类
                        var packageAtt = att as PackageAttribute;

                        var length = packageAtt.Length;

                        var valuestr = content.ChineseCharacterSubstring(length, out content).Trim();

                        if (pro.PropertyType.IsEnum)
                        {
                            foreach (var eatt in pro.PropertyType.GetCustomAttributes(false))
                            {
                                if (eatt is EnumValeuNumberAttribute)
                                {
                                    var eat = eatt as EnumValeuNumberAttribute;
                                    if (eat.IsChar)
                                    {
                                        var chr = Convert.ToChar(valuestr);

                                        var value = Convert.ChangeType(Enum.Parse(pro.PropertyType, ((int)chr).ToString()), pro.PropertyType);
                                        pro.SetValue(entity, value, null);
                                    }
                                    else
                                    {
                                        var value = Convert.ChangeType(Enum.Parse(pro.PropertyType, valuestr), pro.PropertyType);
                                        pro.SetValue(entity, value, null);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            var value = Convert.ChangeType(valuestr, pro.PropertyType);
                            pro.SetValue(entity, value, null);
                        }
                    }
                }
            }
            return (Entity)entity;
        }
    }
}
