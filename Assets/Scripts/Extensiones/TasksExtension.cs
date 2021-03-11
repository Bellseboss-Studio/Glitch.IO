using System.Collections;
using System.Threading.Tasks;


namespace Assets.Scripts.Extensiones
{

    public static class TasksExtension
    {
        public static async void WrapErrors(this Task task)
        {
            await task;
        }

        public static IEnumerator AsCorutine(this Task taks)
        {
            while (!taks.IsCompleted)
            {
                yield return null;
            }

            if (taks.IsFaulted)
            {
                throw taks.Exception;
            }
        }
    }
}
