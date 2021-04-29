using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.Util.PowerShell
{
    public class CliException : ApplicationException
    {
        /// <summary>
        /// Connection Exception Reason
        /// </summary>
        public enum Reason
        {
            Initialze,
            AddSnapIn,
            Connection,
            Session,
            ViewBroker,
            PolicySetting,
            ImportModule,
            GetPool,
            SetPool,
            NewPool,
            GetData,
            UserAssign,
            DeleteMachine,
            ResetMachine,
            EditPool
        }

        private Reason mReason;

        #region Properties

        /// <summary>
        /// 오류 원인을 가져옵니다.
        /// </summary>
        public Reason ErrorReason
        {
            get
            {
                return mReason;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="reason"></param>
        public CliException(Reason reason)
            : base()
        {
            mReason = reason;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="message"></param>
        public CliException(Reason reason, string message)
            : base(message)
        {
            mReason = reason;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CliException(Reason reason, string message, Exception innerException)
            : base(message, innerException)
        {
            mReason = reason;
        }

        #endregion
    }
}
