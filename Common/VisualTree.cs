using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ImageViewer.Common
{
    public static class VisualTree
    {
        /// <summary>
        /// 親要素を取得
        /// </summary>
        /// <param name="obj">起点となるオブジェクト</param>
        /// <returns>親要素</returns>
        public static IEnumerable<DependencyObject> Parent(this DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var parent = obj;

            while (true)
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent == null)
                {
                    yield break;
                }
                else
                {
                    yield return parent;
                }
            }
        }

        /// <summary>
        /// 子要素を取得
        /// </summary>
        /// <param name="obj">起点となるオブジェクト</param>
        /// <returns>子要素</returns>
        public static IEnumerable<DependencyObject> Children(this DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var count = VisualTreeHelper.GetChildrenCount(obj);

            if (count == 0)
            {
                yield break;
            }

            for (int index = 0; index < count; ++index)
            {
                var child = VisualTreeHelper.GetChild(obj, index);

                if (child != null)
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// 子孫要素を取得
        /// </summary>
        /// <param name="obj">起点となるオブジェクト</param>
        /// <returns>子孫要素</returns>
        public static IEnumerable<DependencyObject> Descendants(this DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            foreach (var child in obj.Children())
            {
                yield return child;

                foreach (var grandChild in child.Descendants())
                {
                    yield return grandChild;
                }
            }
        }

        /// <summary>
        /// 特定の型の親要素を取得
        /// </summary>
        /// <param name="obj">起点となるオブジェクト</param>
        /// <returns>親要素</returns>
        public static IEnumerable<T> Parents<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.Parent().OfType<T>();
        }

        /// <summary>
        /// 特定の型の子要素を取得
        /// </summary>
        /// <param name="obj">起点となるオブジェクト</param>
        /// <returns>子要素</returns>
        public static IEnumerable<T> Children<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.Children().OfType<T>();
        }

        /// <summary>
        /// 特定の型の子孫要素を取得
        /// </summary>
        /// <param name="obj">起点となるオブジェクト</param>
        /// <returns>子孫要素</returns>
        public static IEnumerable<T> Descendants<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.Descendants().OfType<T>();
        }
    }
}
