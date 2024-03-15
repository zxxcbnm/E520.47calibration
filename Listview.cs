using System;
using System.Reflection;
using System.Windows.Forms;

namespace E520._47标定
{
    public static class DoubleBufferListView
    {
        /// <summary>
        /// 双缓冲，解决闪烁问题
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="flag"></param>
        public static void DoubleBufferedListView(this ListView lv, bool flag)
        {
            Type lvType = lv.GetType();
            PropertyInfo pi = lvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(lv, flag, null);
        }

    }
}
