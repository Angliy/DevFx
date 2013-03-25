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
    //问题管理接口实现类
    public class QuestionManager : IQuestionManager
    {
        internal protected QuestionManager()
        {
        }


        #region private members

        private IConfigSetting setting;
        /// <summary>
        /// 是否已初始化
        /// </summary>
        protected bool initialized;

        private CollectionBase<Question> questions;

        #endregion


        /// <summary>
        /// 初始化，由框架调用
        /// </summary>
        /// <param name="setting"></param>
        public void Init(IConfigSetting setting)
        {
            if (!this.initialized)
            {
                this.setting = setting;
                this.questions = new CollectionBase<Question>();

                IConfigSetting[] questionSettings = setting["questions"].GetChildSettings();
                for (int i = 0; i < questionSettings.Length; i++)
                {
                    string id = questionSettings[i].Property["id"].Value;
                    if (this.questions.Contains(id))
                    {
                        throw new Exception("重复加载");
                    }
                    Question question = new Question();
                    question.ID = questionSettings[i].Property["id"].Value;
                    question.Text = questionSettings[i].Property["text"].Value;
                    question.Answer = questionSettings[i].Property["answer"].Value;

                    this.questions.Add(id,question);
                }
                this.initialized = true;
            }
        }

        /// <summary>
        /// 用户答题记录
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public void WriteLog(int index, string message)
        {
            //DevFx自带log模块记录
            //HTB.DevFx.Log.Loggor.WriteLog(index, message);

            //log4net记录日志
            LogHelper.WriteLog(message);

        }

        /// <summary>
        /// 获取问题集合
        /// </summary>
        /// <returns></returns>
        public CollectionBase<Question> GetQuestions()
        {
            return questions;
        }



    }
}
