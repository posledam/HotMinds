using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HotMinds.Enums;
using HotMinds.Extensions;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class EnumUtils_Tests
    {
        [Test]
        public void GetMetadata_Generic_Test()
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            Assert.That(() => EnumUtils.GetMetadata<NotEnum>(),
                Throws.InvalidOperationException);

            var list = EnumUtils.GetMetadata<TestEnum>();

            Assert.That(list, Is.Not.Null
                .And.Count.EqualTo(6)
                .And.EquivalentTo(list.OrderBy(p => p.Order).ThenBy(p => p.Enum)));
        }

        [Test]
        public void GetMetadata_Type_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(() => EnumUtils.GetMetadata((Type)null), Throws.ArgumentNullException
                .With.Property("ParamName").EqualTo("enumType"));

            Assert.That(() => EnumUtils.GetMetadata(typeof(NotEnum)), Throws.ArgumentException
                .With.Property("ParamName").EqualTo("enumType"));

            var list = EnumUtils.GetMetadata(typeof(TestEnum));

            Assert.That(list, Is.Not.Null
                .And.Count.EqualTo(6)
                .And.EquivalentTo(list.OrderBy(p => p.Order).ThenBy(p => p.Enum)));
        }

        [Test]
        public void GetMetadata_Generic_Value_Test()
        {
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            Assert.That(() => EnumUtils.GetMetadata<NotEnum>(new NotEnum()), Throws
                .InvalidOperationException);

            // требуется для проверки покрытия кода
            EnumUtils.EnumCache.Clear();
            EnumUtils.FieldCache.Clear();

            Assert.That(EnumUtils.GetMetadata(TestEnum.V0), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // повторный вызов должен вернуть тоже самое
            Assert.That(EnumUtils.GetMetadata(TestEnum.V0), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // вызовы для другого Enum с таким же числовым значением
            Assert.That(EnumUtils.GetMetadata(TestEnum2.V20), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum2.V20)
                .And.Property("Value").EqualTo(TestEnum2.V20)
                .And.Property("Name").EqualTo("V20"));
            Assert.That(EnumUtils.GetMetadata(TestEnum2.V22), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum2.V22)
                .And.Property("Value").EqualTo(TestEnum2.V22)
                .And.Property("Name").EqualTo("V22"));

            // извлечение мета-данных для перечисления с повторяющимся числовым значением
            Assert.That(EnumUtils.GetMetadata(TestEnum.V5), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V5)
                .And.Property("Value").EqualTo(TestEnum.V5)
                // метаданные для V1, а не V5, т.к. у них одиаковые числовые значения,
                // а V1 идёт после V5 в сортировке по умолчанию
                .And.Property("Name").EqualTo("V1")
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);
        }

        [Test]
        public void GetMetadata_Type_Value_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(() => EnumUtils.GetMetadata((Enum)null), Throws
                .ArgumentNullException
                .With.Property("ParamName").EqualTo("value"));

            // требуется для проверки покрытия кода
            EnumUtils.EnumCache.Clear();
            EnumUtils.FieldCache.Clear();

            Assert.That(EnumUtils.GetMetadata((Enum)TestEnum.V0), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // повторный вызов должен вернуть тоже самое
            Assert.That(EnumUtils.GetMetadata((Enum)TestEnum.V0), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // вызовы для другого Enum с таким же числовым значением
            Assert.That(EnumUtils.GetMetadata((Enum)TestEnum2.V20), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum2.V20)
                .And.Property("Value").EqualTo(TestEnum2.V20)
                .And.Property("Name").EqualTo("V20"));
            Assert.That(EnumUtils.GetMetadata((Enum)TestEnum2.V22), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum2.V22)
                .And.Property("Value").EqualTo(TestEnum2.V22)
                .And.Property("Name").EqualTo("V22"));

            // извлечение мета-данных для перечисления с повторяющимся числовым значением
            Assert.That(EnumUtils.GetMetadata((Enum)TestEnum.V5), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V5)
                .And.Property("Value").EqualTo(TestEnum.V5)
                // метаданные для V1, а не V5, т.к. у них одиаковые числовые значения,
                // а V1 идёт после V5 в сортировке по умолчанию
                .And.Property("Name").EqualTo("V1")
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);
        }

        [Test]
        public void GetMetadata_Generic_String_Value_Test()
        {
            Assert.That(() => EnumUtils.GetMetadata<NotEnum>("V0"), Throws
                .InvalidOperationException);

            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once RedundantCast
            Assert.That(() => EnumUtils.GetMetadata<TestEnum>((string)null), Throws
                .ArgumentNullException
                .With.Property("ParamName").EqualTo("name"));

            // по умолчанию поиск должен работать с учётом регистра букв
            Assert.That(EnumUtils.GetMetadata<TestEnum>("v0"), Is.Null);

            // успешный результат поиска с учётом регистра букв
            Assert.That(EnumUtils.GetMetadata<TestEnum>("V0"), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // успешный результат поиска без учёта регистра букв
            Assert.That(EnumUtils.GetMetadata<TestEnum>("v0", true), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // нахождение разных мета-данных для перечислений с одинаковым числовым значением

            Assert.That(EnumUtils.GetMetadata<TestEnum>("V5"), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V5)
                .And.Property("Value").EqualTo(TestEnum.V5)
                .And.Property("Enum").EqualTo(TestEnum.V1)  // одинаковые значения 
                .And.Property("Value").EqualTo(TestEnum.V1) //
                .And.Property("Name").EqualTo("V5") // должно быть V5
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);

            Assert.That(EnumUtils.GetMetadata<TestEnum>("V1"), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V1)
                .And.Property("Value").EqualTo(TestEnum.V1)
                .And.Property("Enum").EqualTo(TestEnum.V5)  // одинаковые значения
                .And.Property("Value").EqualTo(TestEnum.V5) // 
                .And.Property("Name").EqualTo("V1") // должно быть V1
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);
        }

        [Test]
        public void GetMetadata_Type_String_Value_Test()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(() => EnumUtils.GetMetadata(null, "V0"), Throws
                .ArgumentNullException
                .With.Property("ParamName").EqualTo("enumType"));

            Assert.That(() => EnumUtils.GetMetadata(typeof(NotEnum), "V0"), Throws
                .ArgumentException
                .With.Property("ParamName").EqualTo("enumType"));

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(() => EnumUtils.GetMetadata(typeof(TestEnum), null), Throws
                .ArgumentNullException
                .With.Property("ParamName").EqualTo("name"));

            // по умолчанию поиск должен работать с учётом регистра букв
            Assert.That(EnumUtils.GetMetadata(typeof(TestEnum), "v0"), Is.Null);

            // успешный результат поиска с учётом регистра букв
            Assert.That(EnumUtils.GetMetadata(typeof(TestEnum), "V0"), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // успешный результат поиска без учёта регистра букв
            Assert.That(EnumUtils.GetMetadata(typeof(TestEnum), "v0", true), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // нахождение разных мета-данных для перечислений с одинаковым числовым значением

            Assert.That(EnumUtils.GetMetadata(typeof(TestEnum), "V5"), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V5)
                .And.Property("Value").EqualTo(TestEnum.V5)
                .And.Property("Enum").EqualTo(TestEnum.V1)  // одинаковые значения 
                .And.Property("Value").EqualTo(TestEnum.V1) //
                .And.Property("Name").EqualTo("V5") // должно быть V5
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);

            Assert.That(EnumUtils.GetMetadata(typeof(TestEnum), "V1"), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V1)
                .And.Property("Value").EqualTo(TestEnum.V1)
                .And.Property("Enum").EqualTo(TestEnum.V5)  // одинаковые значения
                .And.Property("Value").EqualTo(TestEnum.V5) // 
                .And.Property("Name").EqualTo("V1") // должно быть V1
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);
        }

        [Test]
        public void GetDisplayName_Generic_Test()
        {
            Assert.That(EnumUtils.GetDisplayName(TestEnum.V0), Is.EqualTo("V0"));
            Assert.That(EnumUtils.GetDisplayName(TestEnum.V1), Is.EqualTo("Value 1"));
            Assert.That(EnumUtils.GetDisplayName(TestEnum.V2), Is.EqualTo("Value 2"));
            Assert.That(EnumUtils.GetDisplayName(TestEnum.V3), Is.EqualTo("Value 3"));
            Assert.That(EnumUtils.GetDisplayName(TestEnum.V4), Is.EqualTo("Value 4"));
            // для перечислений с повторяющимися числовыми значениями должно быть
            // взято последнее значение перечисления в отсортированном по умолчанию списке
            Assert.That(EnumUtils.GetDisplayName(TestEnum.V5), Is.EqualTo("Value 1"));
        }

        [Test]
        public void GetDisplayName_Type_Test()
        {
            // ReSharper disable once RedundantCast
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(() => EnumUtils.GetDisplayName((Enum)null), Throws
                .ArgumentNullException
                .With.Property("ParamName").EqualTo("value"));

            Assert.That(EnumUtils.GetDisplayName((Enum)TestEnum.V0), Is.EqualTo("V0"));
            Assert.That(EnumUtils.GetDisplayName((Enum)TestEnum.V1), Is.EqualTo("Value 1"));
            Assert.That(EnumUtils.GetDisplayName((Enum)TestEnum.V2), Is.EqualTo("Value 2"));
            Assert.That(EnumUtils.GetDisplayName((Enum)TestEnum.V3), Is.EqualTo("Value 3"));
            Assert.That(EnumUtils.GetDisplayName((Enum)TestEnum.V4), Is.EqualTo("Value 4"));
            // для перечислений с повторяющимися числовыми значениями должно быть
            // взято последнее значение перечисления в отсортированном по умолчанию списке
            Assert.That(EnumUtils.GetDisplayName((Enum)TestEnum.V5), Is.EqualTo("Value 1"));
        }

        [Test]
        public void ForFilter_Extension_Null_Test()
        {
            IEnumerable<EnumMetadata> all = null;
            IEnumerable<EnumMetadata<TestEnum>> genericAll = null;

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.That(() => all.ForFilter(), Throws.ArgumentNullException);
            Assert.That(() => all.ForFilter(true, true), Throws.ArgumentNullException);
            Assert.That(() => all.ForFilter(false), Throws.ArgumentNullException);
            Assert.That(() => all.ForFilter(false, true), Throws.ArgumentNullException);

            Assert.That(() => genericAll.ForFilter(), Throws.ArgumentNullException);
            Assert.That(() => genericAll.ForFilter(true, true), Throws.ArgumentNullException);
            Assert.That(() => genericAll.ForFilter(false), Throws.ArgumentNullException);
            Assert.That(() => genericAll.ForFilter(false, true), Throws.ArgumentNullException);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void ForFilter_Extension_Generic_Test()
        {
            var all = EnumUtils.GetMetadata<TestEnum>();

            Assert.That(all.ForFilter(), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateFilter() ?? true)));
            Assert.That(all.ForFilter(true, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateFilter() ?? false)));
            Assert.That(all.ForFilter(false), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateFilter() ?? false))));
            Assert.That(all.ForFilter(false, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateFilter() ?? true))));
        }

        [Test]
        public void ForFilter_Extension_Type_Test()
        {
            var all = EnumUtils.GetMetadata(typeof(TestEnum));

            Assert.That(all.ForFilter(), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateFilter() ?? true)));
            Assert.That(all.ForFilter(true, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateFilter() ?? false)));
            Assert.That(all.ForFilter(false), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateFilter() ?? false))));
            Assert.That(all.ForFilter(false, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateFilter() ?? true))));
        }

        [Test]
        public void ForField_Extension_Null_Test()
        {
            IEnumerable<EnumMetadata> all = null;
            IEnumerable<EnumMetadata<TestEnum>> genericAll = null;

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.That(() => all.ForField(), Throws.ArgumentNullException);
            Assert.That(() => all.ForField(true, true), Throws.ArgumentNullException);
            Assert.That(() => all.ForField(false), Throws.ArgumentNullException);
            Assert.That(() => all.ForField(false, true), Throws.ArgumentNullException);

            Assert.That(() => genericAll.ForField(), Throws.ArgumentNullException);
            Assert.That(() => genericAll.ForField(true, true), Throws.ArgumentNullException);
            Assert.That(() => genericAll.ForField(false), Throws.ArgumentNullException);
            Assert.That(() => genericAll.ForField(false, true), Throws.ArgumentNullException);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void ForField_Extension_Generic_Test()
        {
            var all = EnumUtils.GetMetadata<TestEnum>();

            Assert.That(all.ForField(), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateField() ?? true)));
            Assert.That(all.ForField(true, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateField() ?? false)));
            Assert.That(all.ForField(false), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateField() ?? false))));
            Assert.That(all.ForField(false, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateField() ?? true))));
        }

        [Test]
        public void ForField_Extension_Type_Test()
        {
            var all = EnumUtils.GetMetadata(typeof(TestEnum));

            Assert.That(all.ForField(), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateField() ?? true)));
            Assert.That(all.ForField(true, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => p.Display?.GetAutoGenerateField() ?? false)));
            Assert.That(all.ForField(false), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateField() ?? false))));
            Assert.That(all.ForField(false, true), Is.Not.Null
                .And.EquivalentTo(all.Where(p => !(p.Display?.GetAutoGenerateField() ?? true))));
        }

        [Test]
        public void GetMetadata_Extension_Value_Test()
        {
            Assert.That(() => ((Enum)null).GetMetadata(), Throws
                .ArgumentNullException
                .With.Property("ParamName").EqualTo("value"));

            Assert.That(TestEnum.V0.GetMetadata(), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // повторный вызов должен вернуть тоже самое
            Assert.That(TestEnum.V0.GetMetadata(), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V0)
                .And.Property("Value").EqualTo(TestEnum.V0)
                .And.Property("Name").EqualTo("V0")
                .And.Property("Display").Null
                .And.Property("MemberInfo").Not.Null);

            // вызовы для другого Enum с таким же числовым значением
            Assert.That(TestEnum2.V20.GetMetadata(), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum2.V20)
                .And.Property("Value").EqualTo(TestEnum2.V20)
                .And.Property("Name").EqualTo("V20"));
            Assert.That(TestEnum2.V22.GetMetadata(), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum2.V22)
                .And.Property("Value").EqualTo(TestEnum2.V22)
                .And.Property("Name").EqualTo("V22"));

            // извлечение мета-данных для перечисления с повторяющимся числовым значением
            Assert.That(TestEnum.V5.GetMetadata(), Is.Not.Null
                .And.Property("Enum").EqualTo(TestEnum.V5)
                .And.Property("Value").EqualTo(TestEnum.V5)
                // метаданные для V1, а не V5, т.к. у них одиаковые числовые значения,
                // а V1 идёт после V5 в сортировке по умолчанию
                .And.Property("Name").EqualTo("V1")
                .And.Property("Display").Not.Null
                .And.Property("MemberInfo").Not.Null);
        }

        [Test]
        public void GetDisplayName_Extension_Test()
        {
            Enum enumNull = null;

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.That(() => enumNull.GetDisplayName(), Throws.ArgumentNullException);

            Assert.That(TestEnum.V0.GetDisplayName(), Is.EqualTo("V0"));
            Assert.That(TestEnum.V1.GetDisplayName(), Is.EqualTo("Value 1"));
            Assert.That(TestEnum.V2.GetDisplayName(), Is.EqualTo("Value 2"));
            Assert.That(TestEnum.V3.GetDisplayName(), Is.EqualTo("Value 3"));
            Assert.That(TestEnum.V4.GetDisplayName(), Is.EqualTo("Value 4"));
            // для перечислений с повторяющимися числовыми значениями должно быть
            // взято последнее значение перечисления в отсортированном по умолчанию списке
            Assert.That(TestEnum.V5.GetDisplayName(), Is.EqualTo("Value 1"));
        }

        // основной тестируемый Enum
        public enum TestEnum
        {
            // no display attribute
            V0,

            // order to bottom (default == 0)
            [Display(Name = "Value 1", AutoGenerateField = true)]
            V1,

            [Display(Name = "Value 2", AutoGenerateField = false, Order = 1)]
            V2,

            // order to top (default == 0)
            [Display(Name = "Value 3", AutoGenerateFilter = true, Order = -1)]
            V3,

            [Display(Name = "Value 4", AutoGenerateFilter = false)]
            V4,

            [Display(Name = "Value 5")]
            V5 = 1 // double value (alias)
        }

        // дополнительный Enum для проверки правильного извлечения по числовому значению и по типу
        public enum TestEnum2
        {
            // такое же значение, как у TestEnum.V0
            V20,

            // такое же значение, как у TestEnum.V2
            V22 = 2
        }

        // для тестирования подстановки типа вместо Enum
        public struct NotEnum
        {
            public int SomeValue { get; set; }
        }
    }
}