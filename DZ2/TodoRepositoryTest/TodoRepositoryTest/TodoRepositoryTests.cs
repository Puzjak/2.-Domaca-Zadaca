using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories;
using Interfaces;
using MatijaClassLibrary;
using Models;

namespace TodoRepositoryTest
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }
        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }


        [TestMethod]        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }
        [TestMethod]
        public void GetWillReturnNullIfItemNotFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            var randId = Guid.NewGuid();
            var temp = repository.Get(randId);
            Assert.AreEqual(null, temp);
        }

        [TestMethod]
        public void GetWillReturnItemIfFound()
        {
            ITodoRepository repository = new TodoRepository();
            Guid id1, id2, id3;
            var todoItem1 = new TodoItem(" Groceries ");
            id1 = todoItem1.Id;
            repository.Add(todoItem1);
            var todoItem2 = new TodoItem(" Krumpir ");
            id2 = todoItem2.Id;
            repository.Add(todoItem2);
            var todoItem3 = new TodoItem(" Kulen ");
            id3 = todoItem3.Id;
            repository.Add(todoItem3);
            Assert.AreEqual(todoItem1, repository.Get(id1));
            Assert.AreEqual(todoItem2, repository.Get(id2));
            Assert.AreEqual(todoItem3, repository.Get(id3));
        }

        [TestMethod]
        public void GetActiveWillReturnEmptyListIfActiveItemsNotFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            todoItem.IsCompleted = true;
            repository.Add(todoItem);
            var temp = repository.GetActive();
            Assert.IsTrue(!temp.Any());
            
        }

        [TestMethod]
        public void GetActiveWillReturnListIfFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem(" Groceries ");
            todoItem1.IsCompleted = true;
            repository.Add(todoItem1);
            var todoItem2 = new TodoItem(" Krumpir ");
            repository.Add(todoItem2);
            var todoItem3 = new TodoItem(" Kulen ");
            todoItem3.IsCompleted = true;
            repository.Add(todoItem3);
            var temp = repository.GetActive();
            foreach (var element in temp)
            {
                Assert.AreEqual(element.IsCompleted, false);
            }
        }

        [TestMethod]
        public void GetActiveWillReturnEmptyListIfRepositoryIsEmpty()
        {
            ITodoRepository repository = new TodoRepository();
            var temp = repository.GetAll();
            Assert.IsTrue(!temp.Any());
        }

        [TestMethod]
        public void GetAllWillReturnWholeList()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem[] todoItem = new TodoItem[3];
            todoItem[0] = new TodoItem(" Groceries ");
            todoItem[0].IsCompleted = true;
            repository.Add(todoItem[0]);
            todoItem[1] = new TodoItem(" Krumpir ");
            repository.Add(todoItem[1]);
            todoItem[2] = new TodoItem(" Kulen ");
            todoItem[2].IsCompleted = true;
            repository.Add(todoItem[2]);
            var temp = repository.GetAll();
            int i = 0;
            foreach (var element in temp)
            {
                Assert.AreEqual(element, todoItem[i++]);
            }
        }

        [TestMethod]
        public void GetCompletedWillReturnEmptyListIfActiveItemsNotFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            todoItem.IsCompleted = true;
            repository.Add(todoItem);
            var temp = repository.GetActive();
            Assert.IsTrue(!temp.Any());

        }

        [TestMethod]
        public void GetCompletedWillReturnListIfFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem(" Groceries ");
            todoItem1.IsCompleted = true;
            repository.Add(todoItem1);
            var todoItem2 = new TodoItem(" Krumpir ");
            repository.Add(todoItem2);
            var todoItem3 = new TodoItem(" Kulen ");
            todoItem3.IsCompleted = true;
            repository.Add(todoItem3);
            var temp = repository.GetCompleted();
            foreach (var element in temp)
            {
                Assert.AreEqual(element.IsCompleted, true);
            }
        }

        [TestMethod]
        public void MarkAsCompletedWillReturnFalseIfElementNotFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            var randId = Guid.NewGuid();
            Assert.IsFalse(repository.MarkAsCompleted(randId));
        }

        [TestMethod]
        public void MarkAsCompletedWillReturnTrueAndChangeIsCompletedToTrueIfElementIsFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(repository.Get(todoItem.Id).IsCompleted, false);
            Assert.IsTrue(repository.MarkAsCompleted(todoItem.Id));
            Assert.AreEqual(repository.Get(todoItem.Id).IsCompleted, true);
        }

        [TestMethod]
        public void RemoveWillRemoveElementIfFoundAndReturnTrue()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem(" Groceries ");
            repository.Add(todoItem1);
            var todoItem2 = new TodoItem(" Krumpir ");
            repository.Add(todoItem2);
            var todoItem3 = new TodoItem(" Kulen ");
            repository.Add(todoItem3);
            Assert.IsTrue(repository.Remove(todoItem2.Id));
            Assert.AreEqual(repository.Get(todoItem2.Id), null);
        }

        [TestMethod]
        public void RemoveWillReturnFalseIfElementNotFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem(" Groceries ");
            repository.Add(todoItem1);
            var todoItem2 = new TodoItem(" Krumpir ");
            repository.Add(todoItem2);
            var todoItem3 = new TodoItem(" Kulen ");
            repository.Add(todoItem3);
            Assert.IsFalse(repository.Remove(new Guid()));
        }

        [TestMethod]
        public void UpdateWillUpdateItemIfFound()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem1 = new TodoItem(" Groceries ");
            repository.Add(todoItem1);
            var todoItem2 = new TodoItem(" Krumpir ");
            repository.Add(todoItem2);
            var todoItem3 = new TodoItem(" Kulen ");
            repository.Add(todoItem3);
            var temp = new TodoItem("Majoneza");
            temp.Id = todoItem2.Id;
            repository.Update(temp);
            Assert.AreEqual(repository.Get(todoItem2.Id), temp);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NullAsArgumentInUpdateMethodWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Update(null);
        }
    }


}
