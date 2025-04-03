using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;



namespace MirroredChecker
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class DetectMirrored : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // получение документа
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // сбор всех элементов модели
            FilteredElementCollector collector = new FilteredElementCollector(doc)
            .WhereElementIsNotElementType();

            List<Element> mirroredElements = new List<Element>(); // список отзеркаленных элементов
            List<string> elementInfoList = new List<string>(); // список информации об этих элементов

            foreach (Element element in collector) // перебор всех элементов
            {
                if (element is FamilyInstance familyInst)
                {
                    // если элемент отзеркален, добавляем его в список mirroredElements
                    if (familyInst.Mirrored)
                    {
                        mirroredElements.Add(element);

                        // информацию об элементе заносим в elementInfoList
                        string category = familyInst.Category?.Name ?? "Нет категории";
                        string familyName = familyInst.Symbol?.Family?.Name ?? "Нет семейства";

                        string info = $"Имя: {element.Name}, ID: {element.Id}, Категория: {category}, Семейство: {familyName}";
                        elementInfoList.Add(info);
                    }
                }
            }

            if (mirroredElements.Count > 0)
            {
                // выделяем все найденные элементы в интерфейсе (как кликом мыши)
                ICollection<ElementId> mirroredIds = mirroredElements.Select(e => e.Id).ToList();
                uidoc.Selection.SetElementIds(mirroredIds);

                // и так же выводим перечень в виде всплывающего окна
                string messageText = "Отзеркаленные элементы:\n" + string.Join("\n", elementInfoList);
                TaskDialog.Show("Список отзеркаленных элементов", messageText);
            }
            else
            {
                TaskDialog.Show("Результат", "Отзеркаленные элементы не найдены.");
            }

            return Result.Succeeded;
        }
    }
}
