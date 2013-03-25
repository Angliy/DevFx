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
    public class QuestionModule : CoreModule, IFactory, IQuestionManager
    {

        #region constructor

        /// <summary>
        /// 框架初始化
        /// </summary>
		static QuestionModule() {
			Framework.Init();
		}

        private QuestionModule()
        {
			if(current == null) {
				current = this;
			}
		}

		#endregion

		#region private members
        
        private IQuestionManager questionMgr;

        private IQuestionManager CreateQuestionMgr(IConfigSetting setting)
        {
            if (this.questionMgr == null)
            {
                questionMgr = (IQuestionManager)this.setting.Property["type"].ToObject(typeof(IQuestionManager), true);
                questionMgr.Init(setting);
            }
            return this.questionMgr;
        }


		#endregion

		#region override members

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="framework">IFramework</param>
        /// <param name="setting">对应的配置节</param>
        protected override void OnInit(IFramework framework, IConfigSetting setting)
        {
            base.OnInit(framework, setting);
            questionMgr = this.CreateQuestionMgr(this.setting);
        }

       
        /// <summary>
        /// 获取本模块的事例（可以是单例模式也可以是多例模式）
        /// </summary>
        /// <returns>IModule</returns>
        public override IModule GetInstance()
        {
            return Current;
        }

        #endregion

        #region static members

        private static QuestionModule current;

        /// <summary>
        /// 全局管理器的唯一实例（单件模式）
        /// </summary>
        public static QuestionModule Current
        {
            get
            {
                if (current == null)
                {
                    current = new QuestionModule();
                }
                return current;
            }
        }

        public static CollectionBase<Question> GetQuestions()
        {
            return Current.questionMgr.GetQuestions();
        }

        public static void WriteLog(int index,string message)
        {
            Current.questionMgr.WriteLog(index, message);
        }


		#endregion

        #region IQuestionManager Members

        void IQuestionManager.Init(IConfigSetting setting)
        {
            
		}

        void IQuestionManager.WriteLog(int index, string message)
        {
           
		}

        CollectionBase<Question> IQuestionManager.GetQuestions()
        {
            return Current.questionMgr.GetQuestions();
        }
   

		#endregion


		#region IFactory Members

		object IFactory.GetManager(params object[] parameters) {
            return this.questionMgr;
		}

		#endregion
    }
}
