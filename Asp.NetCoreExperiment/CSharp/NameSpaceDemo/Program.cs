//1、嵌套namespace
var demo01Class = new NameSpaceDemo.Demo01Class();
var demo01Class1 = new NameSpaceDemo.NSDemo01.Demo01Class();
var demo01Class2 = new NameSpaceDemo.NSDemo02.Demo01Class();
//2、文件内namespace
var demo02class = new NameSpaceDemo2.Demo02Class();
//3、全局namespace
var demo03class = new Demo03Class();
WriteLine("using global Demo03Class");
