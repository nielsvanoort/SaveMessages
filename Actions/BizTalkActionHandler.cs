using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMessages.MessageExtraction;
using SaveMessages.ExtensionMethods;

namespace SaveMessages.Actions
{
    public class BizTalkActionHandler
    {
        public string ConnectionString;

        public void Resume(Instance instance)
        {
            string stpcd =
            @"exec ops_OperateOnInstances 
                    @snOperation=3,
                    @fMultiMessagebox=0,
                    @uidInstanceID='"+instance.InstanceID.ToString() +@"',
                    @nvcApplication=N'',
                    @snApplicationOperator=0,
                    @nvcHost=N'',
                    @snHostOperator=0,
                    @nServiceClass=111,
                    @snServiceClassOperator=0,
                    @uidServiceType='00000000-0000-0000-0000-000000000000',
                    @snServiceTypeOperator=0,
                    @nStatus=495,
                    @snStatusOperator=1,
                    @nPendingOperation=1,
                    @snPendingOperationOperator=0,
                    @dtPendingOperationTimeFrom='1753-01-01 00:00:00',
                    @dtPendingOperationTimeUntil='9999-12-31 22:59:59.997',
                    @dtStartFrom='1753-01-01 00:00:00',
                    @dtStartUntil='9999-12-31 22:59:59.997',
                    @nvcErrorCode=N'',
                    @snErrorCodeOperator=0,
                    @nvcErrorDescription=N'',
                    @snErrorDescriptionOperator=0,
                    @nvcURI=N'',
                    @snURIOperator=0,
                    @dtStartSuspend='1753-01-01 00:00:00',
                    @dtEndSuspend='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +@"',
                    @nvcAdapter=N'',
                    @snAdapterOperator=0,
                    @nGroupingCriteria=0,
                    @nGroupingMinCount=0,
                    @nMaxMatches=50,
                    @uidAccessorID='"+Guid.NewGuid().ToString()+@"',
                    @nIsMasterMsgBox=1";

            // @uidAccessorID='52AB0AF0-A6F6-45BF-A3F7-1D73A1804B53'

            stpcd.getDtaDataSet(ConnectionString);

        }

        
        public void Suspend(Instance instance)
        {
            string stpcd =
            @"exec ops_OperateOnInstances 
                    @snOperation=3,
                    @fMultiMessagebox=0,
                    @uidInstanceID='"+instance.InstanceID.ToString().ToUpper() +@"',
                    @nvcApplication=N'',
                    @snApplicationOperator=0,
                    @nvcHost=N'',
                    @snHostOperator=0,
                    @nServiceClass=111,
                    @snServiceClassOperator=0,
                    @uidServiceType='00000000-0000-0000-0000-000000000000',
                    @snServiceTypeOperator=0,
                    @nStatus=495,
                    @snStatusOperator=1,
                    @nPendingOperation=1,
                    @snPendingOperationOperator=0,
                    @dtPendingOperationTimeFrom='1753-01-01 00:00:00',
                    @dtPendingOperationTimeUntil='9999-12-31 22:59:59.997',
                    @dtStartFrom='1753-01-01 00:00:00',
                    @dtStartUntil='9999-12-31 22:59:59.997',
                    @nvcErrorCode=N'',
                    @snErrorCodeOperator=0,
                    @nvcErrorDescription=N'',
                    @snErrorDescriptionOperator=0,
                    @nvcURI=N'',
                    @snURIOperator=0,
                    @dtStartSuspend='1753-01-01 00:00:00',
                    @dtEndSuspend='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +@"',
                    @nvcAdapter=N'',
                    @snAdapterOperator=0,
                    @nGroupingCriteria=0,
                    @nGroupingMinCount=0,
                    @nMaxMatches=50,
                    @uidAccessorID='"+Guid.NewGuid().ToString().ToUpper()+@"',
                    @nIsMasterMsgBox=1";

            // @uidAccessorID='52AB0AF0-A6F6-45BF-A3F7-1D73A1804B53'

            stpcd.getDtaDataSet(ConnectionString);

        }

        
        public void Terminate(Instance instance)
        {
            string stpcd =
            @"exec ops_OperateOnInstances 
                    @snOperation=3,
                    @fMultiMessagebox=0,
                    @uidInstanceID='"+instance.InstanceID.ToString() +@"',
                    @nvcApplication=N'',
                    @snApplicationOperator=0,
                    @nvcHost=N'',
                    @snHostOperator=0,
                    @nServiceClass=111,
                    @snServiceClassOperator=0,
                    @uidServiceType='00000000-0000-0000-0000-000000000000',
                    @snServiceTypeOperator=0,
                    @nStatus=495,
                    @snStatusOperator=1,
                    @nPendingOperation=1,
                    @snPendingOperationOperator=0,
                    @dtPendingOperationTimeFrom='1753-01-01 00:00:00',
                    @dtPendingOperationTimeUntil='9999-12-31 22:59:59.997',
                    @dtStartFrom='1753-01-01 00:00:00',
                    @dtStartUntil='9999-12-31 22:59:59.997',
                    @nvcErrorCode=N'',
                    @snErrorCodeOperator=0,
                    @nvcErrorDescription=N'',
                    @snErrorDescriptionOperator=0,
                    @nvcURI=N'',
                    @snURIOperator=0,
                    @dtStartSuspend='1753-01-01 00:00:00',
                    @dtEndSuspend='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +@"',
                    @nvcAdapter=N'',
                    @snAdapterOperator=0,
                    @nGroupingCriteria=0,
                    @nGroupingMinCount=0,
                    @nMaxMatches=50,
                    @uidAccessorID='"+Guid.NewGuid().ToString()+@"',
                    @nIsMasterMsgBox=1";

            // @uidAccessorID='52AB0AF0-A6F6-45BF-A3F7-1D73A1804B53'

            stpcd.getDtaDataSet(ConnectionString);

        }



    }

}
