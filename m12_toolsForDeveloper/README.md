# Задание к модулю Tools for developer

## Задание 1. Static Code Analize

Создайте собственное правило проверки для одного из инструментов (на выбор):

- _StyleCop_
- _Roslyn_

Варианты правил (вы можете предложить свое):

- Все классы, наследуемые от System.Web.Controller должны иметь суффикс Controller
- Все контроллеры должны быть размечены атрибутом [`Authorize`] – либо весь класс целиком, либо все публичные методы
- Все классы-сущности (публичные классы объявленные в пространстве имен XXX.Entities) должны иметь:
  - Быть public
  - Иметь публичные свойства Id и Name
  - Быть размечены [`DataContract`]

## Задание 2.1. NuGet: основное

В данном задании нам предстоит разработать NuGet пакет для распространения нашей библиотеки. Основа библиотеки может быть любой (даже пустая – с 1-2 классами), главное, чтобы:

- Использовались какие-либо дополнительные .Net Framework сборки, помимо `System`
- Использовался хотя бы 1 сторонний NuGet пакет.
  Сам пакет при установке должен делать следующее:

  - Добавлять все необходимые для работы references:

    - Сама наша библиотека - Дополнительные .Net сборки
    - Сборки из зависимых NuGet-пакетов - Добавлять файл `.html` с описанием библиотеки
    - Добавлять хотя бы 1 секцию / параметр в `.config` файл
      При удалении всё должно корректно откатываться.

## Задание 2.2. NuGet: дополнительное (для желающих «копнуть поглубже»)

В дополнение к заданию 1:

- Добиться того, чтобы .html файл открывался в момент установки пакета в студии
- Если в проекте нет ни одного .config файла, такой файл должен добавляться, но при удалении не удаляться!
