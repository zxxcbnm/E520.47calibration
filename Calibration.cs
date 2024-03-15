using System;
using System.Runtime.InteropServices;
using UInt8 = System.Byte;

namespace E520._47标定
{
    internal class Calibration
    {
        public unsafe struct SSP_DLL_INFO
        {
            void* hDll;
            char* pName; // short name
            char* pPath; // full name
            int nVersionInt;
            char* pVersionText;
        };




        public unsafe struct SSP_CFG_INF0
        {
            unsafe void* pData;
            SSP_DLL_INFO* pDll;
        }
        public unsafe struct SSP_CAL_INF0
        {
            unsafe void* pData;
            SSP_DLL_INFO* pDll;
        }
        public unsafe struct SSP_OBS_LIST
        {
            ushort nTypeCount;
            ushort* aType;
            int nDataCount;
            double** aData;
            // the following is only needed by the SSP_Obs functions
            int nDataSize;
            SSP_CAL_INF0* pCal;
        }

        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_OpenLog(string pFileName, byte cLogLevel, byte bAppend);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SSP_GetDescriptor(Int16 nFamily, UInt8 nProduct, UInt8 nHwVersion, UInt8 nSwVersion);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CFG_Init(SSP_CFG_INF0* pCfg, int nDescr);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CAL_Init(SSP_CAL_INF0* pCal, int nDescr);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CFG_SetNvmByName(SSP_CFG_INF0* pCfg, string pName, ushort nData);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SSP_WriteError(string pFmt);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_ObsInit(SSP_OBS_LIST* pObs, SSP_CAL_INF0* pCal, int nDataSize);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_ObsClear(SSP_OBS_LIST* pObs);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SSP_ObsFree(SSP_OBS_LIST* pObs);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_ObsSetDataByType(SSP_OBS_LIST* pObs, ushort nType, int nDataIdx, double dVal);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_ObsSetDataByName(SSP_OBS_LIST* pObs, string pName, int nDataIdx, double dVal);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CAL_GetObsType(SSP_CAL_INF0* pCal, string pName, ushort* pType);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CAL_Calibrate(SSP_CAL_INF0* pCal, SSP_CFG_INF0* pCfg, ushort nSelect, SSP_OBS_LIST* pObsList, int nMask);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_WriteLog(byte cLogLevel, string pFmt);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CFG_GetNvmByNameOffs(SSP_CFG_INF0* pCfg, string pName, int nOffs, ushort* pData);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CAL_Simulate(SSP_CAL_INF0* pCal, SSP_CFG_INF0* pCfg, int nSelect,
                                                                                 SSP_OBS_LIST* pObsList);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_ObsGetDataByName(SSP_OBS_LIST* pObs, string pName, int nDataIdx, double* pVal);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CFG_GetNvmIdx(SSP_CFG_INF0* pCfg, string pName, ushort* pIdx);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CFG_GetNvmName(SSP_CFG_INF0* pCfg, int nIdx, int* pName);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int SSP_CFG_GetNvmByIdx(SSP_CFG_INF0* pCfg, int nIdx, ushort* pData);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SSP_CFG_Free(SSP_CFG_INF0* pCfg);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SSP_CAL_Free(SSP_CAL_INF0* pCal);
        [DllImport("ssp_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SSP_CloseLog();

    }
}
