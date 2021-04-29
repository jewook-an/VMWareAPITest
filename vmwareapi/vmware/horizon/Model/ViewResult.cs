using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace vmwareapi.vmware.horizon.Model
{
    [Serializable]
    [DataContract]
    public class ViewResult
    {
        #region 생성자
        public ViewResult()
        {
        }
        #endregion

        private string _ErrorDescription { get; set; }

        #region 프로퍼티
        /// <summary>
        /// 오류 설명
        /// </summary>
        [Display(Name = "오류 설명")]
        [Required]
        [DataMember]
        public string ErrorDescription
        {
            get
            {
                return _ErrorDescription == null ? string.Empty : _ErrorDescription;
            }
            set
            {
                _ErrorDescription = value;
            }
        }

        /// <summary>
        /// 성공 여부
        /// </summary>
        [Display(Name = "성공 여부")]
        [Required]
        [DataMember]
        public bool Success
        {
            get
            {
                if (string.IsNullOrEmpty(ErrorDescription) == true) return true;
                return false;
            }
        }

        #endregion
    }
}
