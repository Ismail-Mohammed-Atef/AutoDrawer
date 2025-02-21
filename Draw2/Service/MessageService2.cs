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
    public class MessageService2 : ValueChangedMessage<Project>
    {
        public MessageService2(Project value) : base(value) { }
    }
}
