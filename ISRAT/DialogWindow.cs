using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ISRAT
{
    public static class DialogWindow
    {
        public static MessageBoxResult DeleteDialog()
        {
            string sMessageBoxText = "Вы уверены, что хотите удалить запись?";
            string sCaption = "Удаление";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        public static MessageBoxResult InsertDialog()
        {
            string sMessageBoxText = "Вы уверены, что хотите добавить запись?";
            string sCaption = "Добавление";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        public static MessageBoxResult UpdateDialog()
        {
            string sMessageBoxText = "Вы уверены, что хотите изменить запись?";
            string sCaption = "Изменение";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }
    }
}
