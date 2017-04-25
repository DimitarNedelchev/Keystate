namespace Keystate
{
    using System.Windows.Forms;

    class EntryPoint
    {
        static void Main()
        {
            Interceptor.OnKeyPress += Interceptor_OnKeyPress;
            Interceptor.Hook();

            TrayApp.Instance.UpdateCapsLockState(Control.IsKeyLocked(Keys.Capital));
            Application.Run(TrayApp.Instance);

        }

        private static void Interceptor_OnKeyPress(int keyCode)
        {
            switch ((Keys)keyCode)
            {
                case Keys.Capital:
                    // value is inverted because this will be the previous key state
                    TrayApp.Instance.UpdateCapsLockState(!Control.IsKeyLocked((Keys.Capital)));
                    break;
            }
        }
    }
}
