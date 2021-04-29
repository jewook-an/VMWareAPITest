using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon.Model
{
    public class SummaryPoolModel
    {
        #region 공통

        #endregion

        #region Get-HVPoolSummary
        /// <summary>
        /// 데스크톱 풀 NAME
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 디스플레이 이름
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 삭제 진행 여부
        /// </summary>
        public bool Deleting { get; set; }

        /// <summary>
        /// 풀 유형
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 풀 소스
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 소스 위치
        /// </summary>
        public string ImageSource { get; set; }

        /// <summary>
        /// 사용자 할당 여부
        /// </summary>
        public string UserAssignment { get; set; }

        /// <summary>
        /// 프로비저닝 사용여부
        /// </summary>
        public bool ProvisioningEnabled { get; set; }

        /// <summary>
        /// VM 개수
        /// </summary>
        public int NumMachines { get; set; }

        /// <summary>
        /// 연결 세션
        /// </summary>
        public int NumSessions { get; set; }

        #endregion

        #region Set-HVPool
        /// <summary>
        /// 수정용 데스크톱 풀 NAME
        /// </summary>
        public string PoolName { get; set; }

        /// <summary>
        /// 수정용 데스크톱 풀 Object
        /// </summary>
        public object Pool { get; set; }

        /// <summary>
        /// 변수 키, 값으로 풀 구성 업데이트 (ex. 'base.description')
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 변수 키, 값으로 풀 구성 업데이트 (ex. 'update description')
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// user/Group 글로벌 권한자격 부여
        /// ex) Set-HVPool -PoolName foo -globalEntitlement bar
        /// </summary>
        public string globalEntitlement { get; set; }
        #endregion
    }
}
