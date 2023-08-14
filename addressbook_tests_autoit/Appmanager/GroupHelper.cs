using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        //задаем переменную, чтобы каждый раз название окна не писать
        public static string GROUPWINTITLE = "Group editor";

        public GroupHelper(ApplicationManager manager) : base(manager) 
        {
            
        }

        public void Add(GroupData newGroup)
        {

            OpenGroupsDialogue();
            //Нажимаем на кнопку добавления новой группы
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            //Вводим текст (название группы)
            aux.Send(newGroup.Name);
            //Эмулируем нажатие клавиши "Enter"
            aux.Send("{ENTER}");
            //Нажимаем на кнопку закрытия окна групп
            CloseGroupsDialogue();

        }

        private void CloseGroupsDialogue()
        {
            //Нажимаем на кнопку закрытия окна групп
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        private void OpenGroupsDialogue()
        {
            //Название окна, текст кнопки, идентификатор
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            //Ждем открытия окна с группами
            aux.WinWait(GROUPWINTITLE);
        }

        //Получаем список
        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();   
            OpenGroupsDialogue();
            //Используем метод для работы с элементами SysTreeView
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount", "#0", "");
           for (int i = 0; i <int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetText", "#0|#"+i, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }
            CloseGroupsDialogue();
            return list;
        }
    }
}