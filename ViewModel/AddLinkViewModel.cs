using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.ViewModel
{
    public partial class AddLinkViewModel: ObservableObject
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public ObservableCollection<RealEntity> entityLinks;

        [ObservableProperty]
        public RealEntity? selectedEntity;

        private Entity entity;


        public AddLinkViewModel(Entity owner, PageBlock? data)
        {
            entity = owner;
            entityLinks = LoadEntities();
            if (data is not null && data.Type == ContentType.Link)
            {
                Name = (data.Text);
                selectedEntity = EntityLinks.FirstOrDefault(e => e.Id == data.EntityId && e.Type == data.EntityType);
            }
        }

        private ObservableCollection<RealEntity> LoadEntities()
        {

            var entities = Manager.GetInstance().GetWorld().Children;

            var data = new ObservableCollection<RealEntity>(
                entities
                .SelectMany(t => t.Value.Values)
                .Where(e => !(e.Id == entity.Id && e.Type == entity.Type))
                .OrderBy(e => e.Name)
                );

            return data;
            


        }

    }
}
