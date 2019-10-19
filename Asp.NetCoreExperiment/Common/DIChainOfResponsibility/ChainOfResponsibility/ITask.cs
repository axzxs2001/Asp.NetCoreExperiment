using System;
namespace DIChainOfResponsibility
{
    /// <summary>
    /// 职责链接口
    /// </summary>

    public interface ITask
    {
        bool ExecuteTask(TaskParmeter taskParmeter);
    }
}
