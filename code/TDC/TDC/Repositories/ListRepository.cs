using TDC.Models;

namespace TDC.Repositories
{
    public class ListRepository
    {
        private List<ToDoList> lists;

        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;

        #region constructors
        public ListRepository()
        {
            filePath = Path.Combine(projectPath, "lists.csv");

            #if ANDROID
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // create directory if not doesn't exist already
            }

            filePath = Path.Combine(directoryPath, "lists.csv");
            #endif
            lists = new List<ToDoList>();
            LoadAllListsFromFile();
        }
        #endregion

        #region publics
        public void AddList(ToDoList list)
        {
            lists.Add(list);
            SaveListsToFile();
        }

        public void UpdateList(ToDoList newList, string listId) {
            for (var i = 0; i < lists.Count; i++) {
                if (lists[i].GetId().Equals(listId)) {
                    lists[i] = newList;
                }
            }
            SaveListsToFile();
        }

        public void RemoveList(ToDoList list)
        {
            lists.Remove(list);
            SaveListsToFile();
        }

        public List<ToDoList> GetLists()
        {
            // testing only
            #if ANDROID
                return GetDummyLists();
            #endif
            return lists;
        }

        public ToDoList? GetListFromId(string id)
        {
            return lists.FirstOrDefault(list => id.Equals(list.GetId()));
        }

        #endregion

        #region privates
        //TO-DO: ONLY FOR ANDROID TESTING, REMOVE ONCE DATA BASE WORKS
        private List<ToDoList> GetDummyLists()
        {
            var listDummy = new List<ToDoList>();
            var list1 = new ToDoList("first list");
            list1.AddItem(new ListItem("item 1", true, [], 1));
            list1.AddItem(new ListItem("item 2", false, [], 2));
            list1.AddItem(new ListItem("item 3", true, [], 1));
            list1.AddItem(new ListItem("item 4", false, [], 3));
            list1.AddItem(new ListItem("item 5", true, [], 1));
            list1.AddItem(new ListItem("item 6", false, [], 5));

            var list2 = new ToDoList("second list");
            list2.AddItem(new ListItem("first", false, [], 2));
            list2.AddItem(new ListItem("sec", true, [], 1));
            list2.AddItem(new ListItem("third", true, [], 3));

            listDummy.Add(list1);
            listDummy.Add(list2);
            return listDummy;
        }
        private void SaveListsToFile()
        {
            using (var writer = new StreamWriter(filePath)) // overwrite new lists
            {
                writer.WriteLine("ListID;ListName;ItemDescription;Effort;Done;Members"); // header

                foreach (var todoList in lists)
                {
                    foreach (var item in todoList.GetItems())
                    {
                        writer.WriteLine($"{todoList.GetId()};{todoList.GetName()};{item.GetDescription()};{item.GetEffort()};{item.IsDone()}");
                    }
                }
            }
        }

        private void LoadAllListsFromFile()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).Skip(1); // skip header line

                // dict id -> list object
                var listDict = new Dictionary<string, ToDoList>();

                foreach (var line in lines)
                {
                    var values = line.Split(';');

                    var listId = values[0];
                    var listName = values[1];
                    var itemDescription = values[2];
                    var itemEffort = int.Parse(values[3]);
                    var itemDone = bool.Parse(values[4]);

                    if (!listDict.ContainsKey(listId)) //list not registered in dict yet
                    {
                        var todoList = new ToDoList(listName, listId);
                        listDict[listId] = todoList; // add new list to dict
                    }

                    // add list item from line to corresponding list
                    var todoItem = new ListItem(itemDescription, itemDone, new List<Profile>(), itemEffort);
                    listDict[listId].AddItem(todoItem);
                }

                // get all lists from dict and save to actual buffer
                lists = listDict.Values.ToList();
            }
        }
        #endregion
    }
}
