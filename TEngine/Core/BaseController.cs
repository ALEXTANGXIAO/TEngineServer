using TEngineProto;

namespace TEngine
{
    public abstract class BaseController
    {
        protected RequestCode requestCode = RequestCode.RequestNone;

        protected ActionCode actionCode = ActionCode.ActionNone;

        protected ReturnCode returnCode = ReturnCode.ReturnNone;

        public RequestCode GetRequestCode
        {
            get { return requestCode; }
        }
    }
}
