using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatijaClassLibrary;


namespace Repositories
{
    /// <summary>
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </summary>
    public class TodoRepository : ITodoRepository
    {
        /// <summary>
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </summary>
        private readonly List<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new List<TodoItem>();

        }


        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
                throw new ArgumentNullException($"Can't add null object");
            var temp = _inMemoryTodoDatabase.Where(t => t == todoItem).FirstOrDefault();
            if (temp != null)
                throw new DuplicateTodoItemException("Id" + todoItem.Id.ToString() + " is already in array");
            _inMemoryTodoDatabase.Add(todoItem);

        }

        public TodoItem Get(Guid todoId)
        {
            var temp = _inMemoryTodoDatabase.Where(t => t.Id == todoId).FirstOrDefault();
            return temp;
        }

        public List<TodoItem> GetActive()
        {
            var temp = _inMemoryTodoDatabase.Where(t => t.IsCompleted == false).ToList();
            return temp;
        }

        public List<TodoItem> GetAll()
        {
            var temp = _inMemoryTodoDatabase.Where(t => t != null)
                .OrderByDescending(t => t.DateCreated).ToList();
            return temp;
        }

        public List<TodoItem> GetCompleted()
        {
            var temp = _inMemoryTodoDatabase.Where(t => t.IsCompleted == true).ToList();
            return temp;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            var temp = _inMemoryTodoDatabase.Where(t => filterFunction(t) == true).ToList();
            return temp;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            if (Get(todoId) == null)
                return false;
            else
            {
                Get(todoId).MarkAsCompleted();
                return true;
            }
        }

        public bool Remove(Guid todoId)
        {
            return _inMemoryTodoDatabase.Remove(Get(todoId));
        }

        public void Update(TodoItem todoItem)
        {
            if (Get(todoItem.Id) == null)
            {
                Add(todoItem);
            }
            else
            {
                Remove(todoItem.Id);
                Add(todoItem);
            }
        }
    }

}

