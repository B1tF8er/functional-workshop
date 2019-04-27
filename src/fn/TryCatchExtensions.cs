using System;

namespace fn
{
    internal static class TryCatchExceptions
    {
        internal static void TryCatch(Action tryAction, Action<Exception> catchAction)
        {
            try
            {
                tryAction();
            }
            catch (Exception ex)
            {
                catchAction(ex);
            }
        }

        internal static void TryOrFailFast(Action tryAction) =>
            TryCatch(tryAction, (ex) => Environment.FailFast(ex.Message, ex));
    }
}
