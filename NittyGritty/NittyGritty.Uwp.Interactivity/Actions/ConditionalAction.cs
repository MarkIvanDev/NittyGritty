using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using NittyGritty.Extensions;

namespace NittyGritty.Uwp.Interactivity.Actions
{
    [ContentProperty(Name = nameof(Actions))]
    public sealed class ConditionalAction : DependencyObject, IAction
    {
        public Operators Operator
        {
            get { return (Operators)GetValue(OperatorProperty); }
            set { SetValue(OperatorProperty, value); }
        }

        public static readonly DependencyProperty OperatorProperty =
            DependencyProperty.Register(nameof(Operator), typeof(Operators),
                typeof(ConditionalAction), new PropertyMetadata(Operators.EqualTo));

        public object LeftValue
        {
            get { return (object)GetValue(LeftValueProperty); }
            set { SetValue(LeftValueProperty, value); }
        }

        public static readonly DependencyProperty LeftValueProperty =
            DependencyProperty.Register(nameof(LeftValue), typeof(object),
                typeof(ConditionalAction), new PropertyMetadata(null));

        public object RightValue
        {
            get { return (object)GetValue(RightValueProperty); }
            set { SetValue(RightValueProperty, value); }
        }

        public static readonly DependencyProperty RightValueProperty =
            DependencyProperty.Register(nameof(RightValue), typeof(object),
                typeof(ConditionalAction), new PropertyMetadata(null));

        public ActionCollection Actions
        {
            get
            {
                var actions = base.GetValue(ActionsProperty) as ActionCollection;
                if (actions == null)
                {
                    actions = new ActionCollection();
                    SetValue(ActionsProperty, actions);
                }
                return actions;
            }
        }

        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register(nameof(Actions), typeof(ActionCollection),
                typeof(ConditionalAction), new PropertyMetadata(null));

        private int Compare<T>(T left, T right)
        {
            return Comparer<T>.Default.Compare(left, right);
        }

        public object Execute(object sender, object parameter)
        {
            var leftType = LeftValue?.GetType();
            var rightValue = (RightValue == null) ? null : Convert.ChangeType(RightValue, leftType);
            switch (Operator)
            {
                case Operators.EqualTo:
                default:
                    if (LeftValue.EqualTo(RightValue))
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.NotEqualTo:
                    if (!LeftValue.EqualTo(RightValue))
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.LessThan:
                    if (Compare(LeftValue, rightValue) < 0)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.LessThanOrEqualTo:
                    if (Compare(LeftValue, rightValue) <= 0)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.GreaterThan:
                    if (Compare(LeftValue, rightValue) > 0)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.GreaterThanOrEqualTo:
                    if (Compare(LeftValue, rightValue) >= 0)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.IsNull:
                    if (LeftValue == null)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.IsNotNull:
                    if (LeftValue != null)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.IsTrue:
                    if (bool.TryParse(LeftValue?.ToString(), out var rTrue) && rTrue)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.IsFalse:
                    if (bool.TryParse(LeftValue?.ToString(), out var rFalse) && !rFalse)
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.IsNullOrEmpty:
                    if (string.IsNullOrEmpty(LeftValue?.ToString()))
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
                case Operators.IsNotNullOrEmpty:
                    if (!string.IsNullOrEmpty(LeftValue?.ToString()))
                        Interaction.ExecuteActions(sender, Actions, parameter);
                    break;
            }
            return null;
        }
    }
}
