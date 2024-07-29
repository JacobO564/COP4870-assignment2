using Assignment1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private static Filebase? _instance;
        private static object instanceLock = new object();


        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        public List<Item> Items
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var items = new List<Item>();
                lock (instanceLock)
                {
                    foreach (var appFile in root.GetFiles())
                    {
                        var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(appFile.FullName));
                        if (item != null)
                        {
                            items.Add(item);
                        }

                    }
                }
                return items;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp\Items";
        }

        public int LastId
        {
            get
            {
                if (Items?.Any() ?? false)
                {
                    return Items?.Select(c => c.id)?.Max() ?? 0;
                }
                return 0;
            }
        }

        public Item AddOrUpdate(Item item)
        {
            if(item.id <= 0)
            {
                item.id = LastId + 1;
            }

            //go to the right place]
            string path = $"{_root}\\{item.id}.json";
            lock (instanceLock)
            {

                //if the item has been previously persisted
                if (File.Exists(path))
                {
                    //blow it up
                    File.Delete(path);
                }

                //write the file
                File.WriteAllText(path, JsonConvert.SerializeObject(item));
            }

            //return the item, which now has an id
            return item;
        }

        public bool Delete(string id)
        {
            //go to the right place]
            string path = $"{_root}\\{id}.json";

            lock (instanceLock)
            {
                //if the item has been previously persisted
                if (File.Exists(path))
                {
                    //blow it up
                    File.Delete(path);
                    return true;
                }
            }
            return false;
        }
    }


}

