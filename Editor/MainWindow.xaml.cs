using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Editor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Встановлення шрифту і розміру за замовчуванням
            FontFamilyComboBox.SelectedIndex = 0;
            FontSizeComboBox.SelectedIndex = 0;
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Document.Blocks.Clear();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
            if (openFileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(openFileDialog.FileName).ToLower() == ".rtf")
                        range.Load(fs, DataFormats.Rtf);
                    else
                        range.Load(fs, DataFormats.Text);
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                        range.Save(fs, DataFormats.Rtf);
                    else
                        range.Save(fs, DataFormats.Text);
                }
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Copy();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Cut();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Paste();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Текстовий редактор\nАвтор: Oleksandr", "Про програму");
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null && TextEditor != null)
            {
                string selectedFont = (FontFamilyComboBox.SelectedItem as ComboBoxItem).Content.ToString();

                if (TextEditor.Selection.IsEmpty)
                {
                    // Якщо немає виділеного тексту, застосовуємо до всього тексту
                    TextEditor.FontFamily = new FontFamily(selectedFont);
                }
                else
                {
                    // Якщо є виділений текст, застосовуємо тільки до виділеного
                    TextEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(selectedFont));
                }
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null && TextEditor != null)
            {
                string selectedSize = (FontSizeComboBox.SelectedItem as ComboBoxItem).Content.ToString();
                if (TextEditor.Selection.IsEmpty)
                {
                    // Якщо немає виділеного тексту, застосовуємо до всього тексту
                    TextEditor.FontSize = Convert.ToDouble(selectedSize);
                }
                else
                {
                    // Якщо є виділений текст, застосовуємо тільки до виділеного
                    TextEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, Convert.ToDouble(selectedSize));
                }
            }
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty) is FontWeight currentWeight &&
                currentWeight == FontWeights.Bold)
            {
                TextEditor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                TextEditor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty) is FontStyle currentStyle &&
                currentStyle == FontStyles.Italic)
            {
                TextEditor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                TextEditor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectedText = new TextRange(TextEditor.Selection.Start, TextEditor.Selection.End);
            if (TextEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
            {
                TextEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                TextEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}