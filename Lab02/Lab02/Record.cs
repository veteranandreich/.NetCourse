using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    public class Record
    {
        public Record(string Comment, int Id, string Name, string Description, string Source, string Object, bool Ct, bool It, bool At, DateTime IncludingTime, DateTime LastUpdateTime)
        {
            this.Comment = Comment;
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Source = Source;
            this.Object = Object;
            this.ConfidentialityThreat = Ct;
            this.IntegrityThreat = It;
            this.AccessThreat = At;
            this.IncludingTime = IncludingTime;
            this.LastUpdateTime = LastUpdateTime.Date;
        }
        public Record(int Id, string Name)
        {
            this.Comment = "УБИ." + Id;
            this.Name = Name;
        }
        public string Comment { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Source { get; private set; }
        public string Object { get; private set; }
        public bool ConfidentialityThreat { get; private set; }
        public bool IntegrityThreat { get; private set; }
        public bool AccessThreat { get; private set; }
        public DateTime IncludingTime { get; private set; }
        public DateTime LastUpdateTime { get; private set; }
        public override string ToString()
        {
            return $"ID: {this.Id} \n Наименование: {this.Name} \n Описание: {this.Description} \n Источник угрозы: {this.Source} \n" +
                $"Объект войздействия: {this.Object} \n Угроза конфиденциальности: {this.ConfidentialityThreat} \n Угроза целостности: {this.IntegrityThreat} \n" +
                $"Угроза доступа: {this.AccessThreat} \n Время включения: {this.IncludingTime} \n Последнее обновление: {this.LastUpdateTime.Date.ToString("dd/MM/yyyy")}";
        }
    }
}
