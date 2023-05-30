using Ado.NETTask2.Commands;
using Ado.NETTask2.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.NETTask2.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

      
     
        public RelayCommand ShowAll { get; set; }
        public RelayCommand InsertAuthor { get; set; }
        public RelayCommand DeleteAuthor { get; set; }

        public Reposs AuthorsReposs { get; set; }
        public DataSet AuthorSet { get; set; } = new DataSet();

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(); }
        }

        private int ID;

        public int SelectedId
        {
            get { return ID; }
            set { ID = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            AuthorsReposs = new Reposs();

            AuthorSet = AuthorsReposs.GetAll();

            InsertAuthor = new RelayCommand((obj) =>
            {
                AuthorsReposs.InsertAuthor(Id, FirstName, LastName);
            });

            DeleteAuthor = new RelayCommand((obj) =>
            {
                AuthorsReposs.DeleteAuthor(SelectedId);
            });

            ShowAll = new RelayCommand((obj) =>
            {
                AuthorSet = AuthorsReposs.GetAll();
            });
        }
    }
}
