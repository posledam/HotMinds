using System;
using System.Collections.Generic;
using System.Linq;

namespace HotMinds.Enums
{
    /// <summary>
    ///     Методы расширения для Enum-значений и <see cref="EnumMetadata"/> коллекций, возвращаемых
    ///     методами <see cref="EnumUtils"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Метод расширения для фильтрации коллекций <see cref="EnumMetadata"/> всех доступных значений
        ///     перечисления по значению атрибута <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateFilter"/>.
        /// </summary>
        /// <typeparam name="TEnumData">
        ///     Тип, содержащий информацию о значении перечисления, наследуется от <see cref="EnumMetadata"/>. 
        /// </typeparam>
        /// <param name="sequence">
        ///     Коллекция элементов, содержащая все доступные для фильтрации значения перечисления. </param>
        /// <param name="filterValue">
        ///     Параметр фильтрации: будут выбраны только те элементы коллекции, значение атрибута 
        ///     <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateFilter"/> которых 
        ///     равно указанному параметру. Отсутствие атрибута или значения атрибута интерпретируется в зависимости 
        ///     от стратегии фильтрации, указанной в параметре <paramref name="strict"/>. </param>
        /// <param name="strict">
        ///     Стратегия фильтрации: строгий режим. Если указано true, то элементы коллекции без значения 
        ///     атрибута <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateFilter"/> 
        ///     (или без атрибута) будут отфильтрованы. Если указано false, такие элементы будут считаться подходящими 
        ///     независимо от значения параметра <paramref name="filterValue"/>. </param>
        /// <returns>
        ///     Отфильтрованная коллекция. </returns>
        public static IEnumerable<TEnumData> ForFilter<TEnumData>(this IEnumerable<TEnumData> sequence,
            bool filterValue = true, bool strict = false)
            where TEnumData : EnumMetadata
        {
            return
                from p in sequence
                let autoGenerateFilter = p.Display?.GetAutoGenerateFilter()
                where (!strict && autoGenerateFilter == null) || (autoGenerateFilter == filterValue)
                select p;
        }

        /// <summary>
        ///     Метод расширения для фильтрации коллекций <see cref="EnumMetadata"/> всех доступных значений
        ///     перечисления по значению атрибута <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField"/>.
        /// </summary>
        /// <typeparam name="TEnumData">
        ///     Тип, содержащий информацию о значении перечисления, наследуется от <see cref="EnumMetadata"/>. 
        /// </typeparam>
        /// <param name="sequence">
        ///     Коллекция элементов, содержащая все доступные для фильтрации значения перечисления. </param>
        /// <param name="fieldValue">
        ///     Параметр фильтрации: будут выбраны только те элементы коллекции, значение атрибута 
        ///     <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField"/> которых 
        ///     равно указанному параметру. Отсутствие атрибута или значения атрибута интерпретируется в зависимости 
        ///     от стратегии фильтрации, указанной в параметре <paramref name="strict"/>. </param>
        /// <param name="strict">
        ///     Стратегия фильтрации: строгий режим. Если указано true, то элементы коллекции без значения 
        ///     атрибута <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute.AutoGenerateField"/> 
        ///     (или без атрибута) будут отфильтрованы. Если указано false, такие элементы будут считаться подходящими 
        ///     независимо от значения параметра <paramref name="fieldValue"/>. </param>
        /// <returns>
        ///     Отфильтрованная коллекция. </returns>
        public static IEnumerable<TEnumData> ForField<TEnumData>(this IEnumerable<TEnumData> sequence,
            bool fieldValue = true, bool strict = false)
            where TEnumData : EnumMetadata
        {
            return
                from p in sequence
                let autoGenerateField = p.Display?.GetAutoGenerateField()
                where (!strict && autoGenerateField == null) || (autoGenerateField == fieldValue)
                select p;
        }

        /// <summary>
        ///     Получить объект мета-данных указанного Enum-значения.
        /// </summary>
        /// <param name="value">
        ///     Значение перечисления. </param>
        /// <returns>
        ///     Объект мета-данных Enum-значения.
        /// </returns>
        public static EnumMetadata GetMetadata(this Enum value)
        {
            return EnumUtils.GetMetadata(value);
        }

        /// <summary>
        ///     Получить локализованное название Enum-значения.
        /// </summary>
        /// <param name="value">
        ///     Значение перечисления. </param>
        /// <returns>
        ///     Текст локализованного названия Enum-значение при наличии, или название Enum-значения.
        /// </returns>
        public static string GetDisplayName(this Enum value)
        {
            return EnumUtils.GetDisplayName(value);
        }
    }
}
