using CommunityToolkit.Mvvm.Messaging.Messages;
using Draw2.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw2.Service
{
    public class MessageService : ValueChangedMessage<Project>
    {
        public MessageService(Project value) : base(value) { }
    }
}
