# Задания к модулю BCL

## Общее

В данном задании нам потребуется написать приложение-службу (на самом деле - просто консольное приложение, которое запускается и выполняет работу, а завершается по системному сигналу – нажатию `Ctrl+C` или `Ctrl+Break`), которое будет:

1. Слушать несколько входящих папок (это можно делать или периодическим опросом, или используя класс `FileSystemWatcher`)
2. При появлении в папке файла:

   - Пробегать по внутреннему списку правил, который состоит из пары «шаблон имени файла – папка назначения».
   - При совпадении шаблона перекладывать файл
   - Если нужного шаблона не найдено – перекладывать в папку «по умолчанию»

3. Логгировать на экране свою деятельность. В частности, такие события как:

   - Обнаружение нового файла (его имя и дата создания)
   - Нахождение/не нахождение подходящего правила
   - Перенос файла в папку назначения
   - ….

## Задание 1. Локализация приложения

Разрабатываемое приложение необходимо сделать максимально интернационализуемым:

- Использовать установку требуемой культуры при старте приложения (_см. задание 2_)
- Все строки перенести в ресурсы
- Использовать форматирование дат и других типов данных.

Для демонстрации разработайте как минимум 2 локализации:

- Русская
- Английская

## Задание 2. Конфигурирование

Приложение должно уметь читать свои стартовые настройки из стандартного `application.config`. Для задания настроек создать собственную `Custom configuration section`, в которой максимально использовать подходящие типы данных (т.е. не использовать просто строки!).
В качестве настроек должно быть указано следующее:

1.  Требуемую культуру для интерфейса
2.  Список папок, которые нужно слушать
3.  Список правил, который включает в себя:

- Шаблон имени файла в виде регулярного выражения
- Папка назначения
- Параметры того, как меняется имя выходного файла:

  - Добавлять ли порядковый номер
  - Добавлять ли дату перемещения
