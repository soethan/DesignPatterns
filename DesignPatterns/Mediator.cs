using System;
using System.Collections.Generic;

namespace DesignPatterns.Mediator
{
    /// <summary>
    /// The 'Mediator' abstract class
    /// </summary>
    public interface IChatroom
    {
        void Register(Participant participant);
        void Send(string from, string to, string message);
    }

    /// <summary>
    /// The 'ConcreteMediator' class
    /// </summary>
    public class Chatroom : IChatroom
    {
        private Dictionary<string, Participant> _participants = new Dictionary<string, Participant>();

        public void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public void Send(string from, string to, string message)
        {
            Participant participant = _participants[to];

            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    public class Participant
    {
        private IChatroom _chatroom;
        private string _name;

        // Constructor
        public Participant(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public IChatroom Chatroom
        {
            set { _chatroom = value; }
            get { return _chatroom; }
        }

        // Sends message to given participant
        public void Send(string to, string message)
        {
            _chatroom.Send(_name, to, message);
        }

        // Receives message from given participant
        public virtual void Receive(string from, string message)
        {
            Console.WriteLine("{0} to {1}: '{2}'", from, _name, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    public class GraduateStudentParticipant : Participant
    {
        public GraduateStudentParticipant(string name)
            : base(name)
        {

        }

        public override void Receive(string from, string message)
        {
            Console.Write("To a Graduate Student Participant: ");
            base.Receive(from, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    public class UnderGraduateStudentParticipant : Participant
    {
        public UnderGraduateStudentParticipant(string name)
            : base(name)
        {

        }

        public override void Receive(string from, string message)
        {
            Console.Write("To a Undergraduate Student Participant: ");
            base.Receive(from, message);
        }
    }
}
