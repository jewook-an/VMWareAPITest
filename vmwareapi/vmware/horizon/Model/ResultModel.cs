using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon.Model
{
    /// <summary>
    /// 메시지 리턴
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResultModel<T> // : ISerializable
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public ResultModel()
        {
            Success = true;
            Code = string.Empty;
            Description = string.Empty;
            //Page = new PageModel();
            //Search = new SearchModel();
            Exception = string.Empty;

            // string 등 시스템 정의 object의 경우 처리
            if (typeof(T) == typeof(string))
            {
                Result = default(T);
            }
            else
            {
                Result = Activator.CreateInstance<T>();
            }
        }

        /// <summary>
        /// 성공여부
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// 메시지 코드
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 메시지 설명
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 예외 메시지 
        /// </summary>
        [DataMember]
        public string Exception { get; set; }

        /*
        /// <summary>
        /// 페이징 정보
        /// </summary>
        [DataMember]
        public PageModel Page { get; set; }

        /// <summary>
        /// 검색정보
        /// </summary>
        [DataMember]
        public SearchModel Search { get; set; }
         */

        private T _result;
        /// <summary>
        /// 결과
        /// </summary>
        [DataMember]
        public T Result
        {
            set
            {
                _result = value;
            }
            get
            {
                return _result;
                //if (!Success)
                //{
                //    throw new ResultException() { ErrorMessage = Description, ErrorCode = Code };
                //}
                //else
                //{
                //    return _result;
                //}
            }
        }

        /// <summary>
        /// Result Model 복사
        /// </summary>
        /// <returns></returns>
        //public ResultModelEx<T> Copy()
        //{
        //    ResultModelEx<T> retValue = new ResultModelEx<T>();
        //    retValue.Success = Success;
        //    retValue.Code = Code;
        //    retValue.Description = Description;
        //    retValue.Exception = Exception;
        //    retValue.Page = Page;
        //    retValue.Search = Search;
        //    retValue.Result = Result;
        //    return retValue;
        //}

        #region ISerializable 멤버

        public ResultModel(SerializationInfo info, StreamingContext context)
        {
            GenericSerializationInfo ginfo = new GenericSerializationInfo(info);
            Success = info.GetBoolean("Success");
            Code = info.GetString("Code");
            Description = info.GetString("Description");
            Exception = info.GetString("Exception");
            //Page = (PageModel)info.GetValue("Page", typeof(PageModel));
            //Search = (SearchModel)info.GetValue("Search", typeof(SearchModel));
            //Page = (PageModel)info.GetValue("Page", typeof(PageModel));
            Result = (T)info.GetValue("Result", typeof(T));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Success", Success);
            info.AddValue("Code", Code);
            info.AddValue("Description", Description);
            info.AddValue("Exception", Exception);
            //info.AddValue("Page", Page);
            //info.AddValue("Search", Search);
            info.AddValue("Result", _result);
        }

        public class GenericSerializationInfo
        {
            SerializationInfo m_SerializationInfo;

            public GenericSerializationInfo(SerializationInfo info)
            {
                m_SerializationInfo = info;
            }

            public void AddValue(string name, T value)
            {
                m_SerializationInfo.AddValue(name, value, value.GetType());
            }

            public T GetValue(string name)
            {
                object obj = m_SerializationInfo.GetValue(name, typeof(T));
                return (T)obj;
            }
        }
        #endregion
    }
}
