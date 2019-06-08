using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
namespace WebServiceInvockDemo
{
    /// <summary<
    /// WebService代理类
    /// </summary<
    public class WebServiceAgent
    {
        private object agent;
        private Type agentType;
        private const string CODE_NAMESPACE = "";

    
        /// <summary<
        /// 构造函数
        /// </summary<
        /// <param name="url"<</param<
        public WebServiceAgent(string url)
        {

         
            XmlTextReader reader = new XmlTextReader(url + "?wsdl");
            //创建和格式化 WSDL 文档
            ServiceDescription sd = ServiceDescription.Read(reader);

            //创建客户端代理代理类
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);

            //使用 CodeDom 编译客户端代理类
            CodeNamespace cn = new CodeNamespace(CODE_NAMESPACE);
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            Microsoft.CSharp.CSharpCodeProvider icc = new Microsoft.CSharp.CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            CompilerResults cr = icc.CompileAssemblyFromDom(cp, ccu);
            agentType = cr.CompiledAssembly.GetTypes()[0];
            agent = Activator.CreateInstance(agentType);
        }

        ///<summary<
        ///调用指定的方法
        ///</summary<
        ///<param name="methodName"<方法名，大小写敏感</param<
        ///<param name="args"<参数，按照参数顺序赋值</param<
        ///<returns<Web服务的返回值</returns<
        public object Invoke(string methodName, params object[] args)
        {
         
            MethodInfo mi = agentType.GetMethod(methodName);
            var result= this.Invoke(mi, args);
        
            return result;
        }
        ///<summary<
        ///调用指定方法
        ///</summary<
        ///<param name="method"<方法信息</param<
        ///<param name="args"<参数，按照参数顺序赋值</param<
        ///<returns<Web服务的返回值</returns<-
        public object Invoke(MethodInfo method, params object[] args)
        {            
                return method.Invoke(agent, args);         
        }
        public MethodInfo[] Methods
        {
            get
            {
                return agentType.GetMethods();
            }
        }
    }
}

