using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CommandPattern
{
    /// <summary>
    /// The 'Command' abstract class
    /// </summary>
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class CalculatorCommand : Command
    {
        private char _operator;
        private int _operand;
        private Calculator _calculator;

        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this._calculator = calculator;
            this._operator = @operator;
            this._operand = operand;
        }
      
        public override void Execute()
        {
            _calculator.Operation(_operator, _operand);
        }

        public override void UnExecute()
        {
            _calculator.Operation(Undo(_operator), _operand);
        }

        // Returns opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default: throw new ArgumentException("@operator");
            }
        }
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    public class Calculator
    {
        private int _curr = 0;

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
            case '+': _curr += operand; break;
            case '-': _curr -= operand; break;
            case '*': _curr *= operand; break;
            case '/': _curr /= operand; break;
            }
            Console.WriteLine("Current value = {0,3} (following {1} {2})", _curr, @operator, operand);
        }
    }

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    public class User
    {
        private Calculator _calculator = new Calculator();
        private List<Command> _commands = new List<Command>();
        private int _current = 0;

        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);
            // Perform redo operations
            for (int i = 0; i < levels; i++)
            {
            if (_current < _commands.Count - 1)
            {
                Command command = _commands[_current++];
                command.Execute();
            }
            }
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);
            // Perform undo operations
            for (int i = 0; i < levels; i++)
            {
            if (_current > 0)
            {
                Command command = _commands[--_current] as Command;
                command.UnExecute();
            }
            }
        }

        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            Command command = new CalculatorCommand(_calculator, @operator, operand);
            command.Execute();

            // Add command to commands list
            _commands.Add(command);

            _current++;
        }
    }
}