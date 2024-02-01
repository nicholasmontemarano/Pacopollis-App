namespace backend_app.Controllers
{
    public interface IIsAdminController
    {
        bool IsAdmin(int voterId);
    }
}