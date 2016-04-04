using UnityEngine;
using UnityEditor;

namespace Enigma.LightsHearts.Editors
{
    public static class EditorStyles
    {
        private static GUIStyle _boxStyle;
        public static GUIStyle BoxStyle
        {
            get
            {
                if (_boxStyle == null)
                {
                    _boxStyle = new GUIStyle("HelpBox");
                    _boxStyle.padding = new RectOffset(10, 10, 10, 10);
                }

                return _boxStyle;
            }
        }

        public static string SearchBar(string searchQuery, EditorWindow window, bool isSearching)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.SetNextControlName("SearchField");
            string q = EditorGUILayout.TextField(searchQuery, (GUIStyle)"SearchTextField"); // , GUILayout.Width(width)
            if (isSearching)
            {
                if (GUILayout.Button("", (GUIStyle)"SearchCancelButton", GUILayout.Width(17)))
                {
                    q = ""; // Reset
                    if (window != null)
                        window.Repaint();
                }
            }
            else
            {
                GUILayout.Button("", (GUIStyle)"SearchCancelButtonEmpty", GUILayout.Width(17));
            }

            EditorGUILayout.EndHorizontal();

            return q;
        }
    }
}
