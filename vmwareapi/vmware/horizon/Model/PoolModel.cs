using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon.Model
{
    /// <summary>
    /// InstantClone, LinkedClone, FullClone, Manual, RDS
    /// </summary>
    public class PoolModel
    {
        #region 단독 ([< SwitchParameter >])
        // * 필수
        // -InstantClone
        // -LinkedClone
        // -FullClone
        // -Manual (수동데스크톱 풀) > 전용(옵션-자동할당), 부동
        // -Rds (RDS데스크톱 풀)
        public string PoolType { get; set; }

        // 옵션
        // -WhatIf
        // -Confirm
        #endregion

        #region 필수
        /// <summary>
        /// JSON File이용 Clone시 File Path
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 신규풀 복제를 위한 기존풀정보
        /// </summary>
        public object ClonePool { get; set; }

        /// <summary>
        /// 풀 이름
        /// </summary>
        public string PoolName { get; set; }

        /// <summary>
        /// Floating or Dedicated
        /// </summary>
        public string UserAssignment { get; set; }

        /// <summary>
        /// VM복제를 위한 Template (Full Clone Only)
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// 복제할 상위 VM (Linked Clone and Instant Clone Only)
        /// </summary>
        public string ParentVM { get; set; }

        /// <summary>
        /// Linked Clone Pool의 기본 이미지 VM 및 Instant Clone Pool의 현재 이미지.
        /// </summary>
        public string SnapshotVM { get; set; }

        /// <summary>
        /// VM 배포를 위한 Folder (Full, Linked, Instant Clone Pools)
        /// </summary>
        public string VmFolder { get; set; }

        /// <summary>
        /// VM 배포를 위한 Host, Cluster (Full, Linked, Instant Clone Pools)
        /// </summary>
        public string HostOrCluster { get; set; }

        /// <summary>
        /// VM 배포를 위한 ResourcePool (Full, Linked, Instant Clone Pools)
        /// </summary>
        public string ResourcePool { get; set; }

        /// <summary>
        /// VM저장을 위한 DataStore
        /// </summary>
        public string[] Datastores { get; set; }

        /// <summary>
        /// VM이름 지정 (기본값 : PATTERN) / -NamingPattern 사용 > PATTERN 설정 (Instant Clone의 경우 값은 PATTERN)
        /// </summary>
        public string NamingMethod { get; set; }

        /// <summary>
        /// Domain Net Bios Name
        /// </summary>
        public string NetBiosName { get; set; }

        /// <summary>
        /// 사용할 사용자 정의 유형 ('CLONE_PREP','QUICK_PREP','SYS_PREP','NONE')
        /// </summary>
        public string CustType { get; set; }

        /// <summary>
        /// 수동풀을 위한 VM Source ('VIRTUAL_CENTER', 'UNMANAGED')
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 수동풀 추가 할 기존 VM Name List (Manual Pool)
        /// </summary>
        public string[] VM { get; set; }
        #endregion

        #region 옵션

        public string PoolDisplayName { get; set; }

        public string Description { get; set; }

        public string AccessGroup { get; set; }

        public string GlobalEntitlement { get; set; }

        /// <summary>
        /// 처음 VM Access시 자동할당 - Dedicate Desktop Pool
        /// </summary>
        public bool AutomaticAssignment { get; set; }

        /// <summary>
        /// 풀을 활성화 true / 비활성화 false (Default > True)
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 연결서버 제한사항 - 데스크탑 액세스 가능 TAG 목록(없을땐 모든 연결서버에서 데스크탑 액세스 가능)
        /// </summary>
        public string[] ConnectionServerRestrictions { get; set; }

        /// <summary>
        /// LogOff후 데스크탑 시스템 전원정책(관리되는 컴퓨터에만 해당)
        /// </summary>
        public string PowerPolicy { get; set; }

        /// <summary>
        /// 연결해제 후 자동LogOff(default "NEVER")
        /// </summary>
        public string AutomaticLogoffPolicy { get; set; }

        /// <summary>
        /// 연결해제 후 자동LogOff 시간(분)입니다 (automaticLogoffPolicy가 "AFTER"로 설정된 경우 필요)
        /// </summary>
        public int AutomaticLogoffMinutes { get; set; }

        /// <summary>
        /// 사용자의 머신 재설정/재시작 가능여부
        /// </summary>
        public bool allowUsersToResetMachines { get; set; }

        /// <summary>
        /// 유동 사용자 할당의 경우 사용자 당 여러 세션이 허용되는지 여부
        /// </summary>
        public bool allowMultipleSessionsPerUser { get; set; }

        /// <summary>
        /// LogOff후 삭제, 새로고침 여부
        /// </summary>
        public string deleteOrRefreshMachineAfterLogoff { get; set; }

        /// <summary>
        /// Dedicated 할당, OS 디스크 Refresh여부, 시기 (linked-clone machines)
        /// </summary>
        public string refreshOsDiskAfterLogoff { get; set; }

        public int refreshPeriodDaysForReplicaOsDisk { get; set; }

        public int refreshThresholdPercentageForReplicaOsDisk { get; set; }

        public string[] supportedDisplayProtocols { get; set; }

        public string defaultDisplayProtocol { get; set; }

        public int allowUsersToChooseProtocol { get; set; }

        public bool enableHTMLAccess { get; set; }

        public string renderer3D { get; set; }

        public bool enableGRIDvGPUs { get; set; }

        public int vRamSizeMB { get; set; }

        public int maxNumberOfMonitors { get; set; }

        public string maxResolutionOfAnyOneMonitor { get; set; }

        public string Quality { get; set; }

        public string Throttling { get; set; }

        public bool overrideGlobalSetting { get; set; }

        public bool enabled { get; set; }

        public string url { get; set; }

        public string Vcenter { get; set; }

        public string datacenter { get; set; }

        public string[] StorageOvercommit { get; set; }

        public bool UseVSAN { get; set; }

        public bool UseSeparateDatastoresReplicaAndOSDisks { get; set; }

        public string ReplicaDiskDatastore { get; set; }

        public bool UseNativeSnapshots { get; set; }

        public bool ReclaimVmDiskSpace { get; set; }

        public int ReclamationThresholdGB { get; set; }

        /// <summary>
        /// WindowsProfile >> 영구 디스크로 리디렉션 >> 새로 고침, 재구성 및 재조정과 같은 View Composer 작업의 영향을 받지 않는다
        /// LinkedClone Floating Desktop의 경우 false 이어야 함 (default : True)
        /// </summary>
        public bool RedirectWindowsProfile { get; set; }

        public bool UseSeparateDatastoresPersistentAndOSDisks { get; set; }

        public string[] PersistentDiskDatastores { get; set; }

        public string[] PersistentDiskStorageOvercommit { get; set; }

        public int DiskSizeMB { get; set; }

        public string DiskDriveLetter { get; set; }

        public bool redirectDisposableFiles { get; set; }

        public int NonPersistentDiskSizeMB { get; set; }

        public string NonPersistentDiskDriveLetter { get; set; }

        public bool UseViewStorageAccelerator { get; set; }

        public string ViewComposerDiskTypes { get; set; }

        public int RegenerateViewStorageAcceleratorDays { get; set; }

        // 데스크톱 추가 > 설정 > 고급스토리지 옶션
        //public string BlackoutTimes<DesktopBlackoutTime[]>
        //public string Nics<DesktopNetworkInterfaceCardSettings[]>

        /// <summary>
        /// 프로비저닝 사용
        /// </summary>
        public bool EnableProvisioning { get; set; }

        /// <summary>
        /// 오류시 프로비저닝 중지
        /// </summary>
        public bool StopProvisioningOnError { get; set; }

        /// <summary>
        /// TransparentPage 공유범위 (default "VM")
        /// </summary>
        public string TransparentPageSharingScope { get; set; }

        /// <summary>
        /// VM Name 지정 패턴 (default : poolName + '{n:fixed=4}')
        /// </summary>
        public string NamingPattern { get; set; }

        /// <summary>
        /// 프로비저닝 된 머신 최소수.(View Composer 유지보수작업) (default 0) // LinkedClone
        /// </summary>
        public int MinReady { get; set; }

        /// <summary>
        /// 풀에있는 최대 시스템 수(default 1) - Full,Linked,Instant Clone
        /// </summary>
        public int MaximumCount { get; set; }

        /// <summary>
        /// 풀에서 예비 전원이 켜진 시스템 수.(default 1)
        /// </summary>
        public int SpareCount { get; set; }

        /// <summary>
        /// 프로비저닝 시기 결정 (ON_DEMAND/UP_FRONT)
        /// </summary>
        public string ProvisioningTime { get; set; }

        /// <summary>
        /// 요구시 프로비저닝 >> 프로비저닝 최소수 (default 0)
        /// </summary>
        public int MinimumCount { get; set; }

        public string[] SpecificNames { get; set; }

        public bool StartInMaintenanceMode { get; set; }

        public int NumUnassignedMachinesKeptPoweredOn { get; set; }

        public object AdContainer { get; set; }

        /// <summary>
        /// 도메인 가입시 사용 > 도메인관리자명 (default : null)
        /// </summary>
        public string DomainAdmin { get; set; }

        public bool ReusePreExistingAccounts { get; set; }

        /// <summary>
        /// 사용할 사용자정의 스펙 - Full,Linked Clone
        /// </summary>
        public string SysPrepName { get; set; }

        public bool DoNotPowerOnVMsAfterCreation { get; set; }

        public string PowerOffScriptName { get; set; }

        public string PowerOffScriptParameters { get; set; }

        public string PostSynchronizationScriptName { get; set; }

        public string PostSynchronizationScriptParameters { get; set; }

        public string Farm { get; set; }

        public object HvServer { get; set; }
        #endregion
    }
}
