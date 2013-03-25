using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HTB.DevFx;
using HTB.DevFx.Core;
using HTB.DevFx.Config;
using HTB.DevFx.Utils;



namespace DevFxTest
{   
    //问题管理接口
    public interface IQuestionManager
    {
        /// <summary>
        /// 初始化，由框架调用
        /// </summary>
        /// <param name="setting"></param>
        void Init(IConfigSetting setting);


        /// <summary>
        /// 获取问题集合
        /// </summary>
        /// <returns></returns>
        CollectionBase<Question> GetQuestions();
       


        /// <summary>
        /// 用户答题记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        void WriteLog(int index, string message);
        
    }
}
