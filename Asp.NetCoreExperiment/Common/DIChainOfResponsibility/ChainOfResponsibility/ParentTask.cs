using System;
namespace DIChainOfResponsibility
{

    /// <summary>
    /// 职责链任务抽象类
    /// </summary>
    public abstract class ParentTask
    {


        protected ParentTask _parentTask;
        /// <summary>
        /// 传送下一个方法
        /// </summary>
        /// <param name="parentTask"></param>
        public void Next(ParentTask parentTask)
        {
            Console.WriteLine($"-------------{parentTask.GetType().Name}.Next()");
            _parentTask = parentTask;
        }
        /// <summary>
        /// 任务
        /// </summary>
        /// <param name="taskParmeter"></param>
        /// <returns></returns>
        public abstract bool ExecuteTask(TaskParmeter taskParmeter);
    }
}
