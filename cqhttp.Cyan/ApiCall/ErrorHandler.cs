namespace cqhttp.Cyan.ApiCall {
    /// <summary>处理调用API时遇到错误代码的情况</summary>
    public class ErrorHandler {
        /// <summary>
        /// 根据不正常的API调用结果进行相应的操作
        /// <see>https://docs.cqp.im/dev/v9/errorcode/</see>
        /// </summary>
        /// <param name="apiRetcode">返回代码不正常的API调用</param>
        public static void Handle (int apiRetcode) {
            switch (apiRetcode) {
            case 100: //参数缺失或参数无效，通常是因为没有传入必要参数，某些接口中也可能因为参数明显无效（比如传入的 QQ 号小于等于 0，此时无需调用酷 Q 函数即可确定失败），此项和以下的 status 均为 failed
                throw new Exceptions.ErrorApicallException ("参数缺失或无效");
            case 102: //酷 Q 函数返回的数据无效，一般是因为传入参数有效但没有权限，比如试图获取没有加入的群组的成员列表
            case 103: //操作失败，一般是因为用户权限不足，或文件系统异常、不符合预期
                throw new Exceptions.ErrorApicallException ("请检查酷Q运行环境");
            case 104: //由于酷 Q 提供的凭证（Cookie 和 CSRF Token）失效导致请求 QQ 相关接口失败，可尝试清除酷 Q 缓存来解决
                throw new Exceptions.ErrorApicallException ("酷Q登陆过期，请清除酷Q缓存");
            case 201: //工作线程池未正确初始化（无法执行异步任务）
                break;
            case -26:
                throw new Exceptions.ErrorApicallException ("发送消息过长, 请分条发送");
            case -34:
                Log.Warn ("账号被禁言, 消息无法发送");
                return;
            }
            throw new Exceptions.ErrorApicallException ($"调用API返回的结果有误(retcode:{apiRetcode})");
        }
    }
}