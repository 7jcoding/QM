using QM.Manager.Common;
using System.Threading.Tasks;

namespace QM.Manager {
    public interface IShell {
        Task Show(BaseScreen screen);
        Task ShowDialog(BaseScreen screen, int width, int height);
        void HideDialog();
    }
}