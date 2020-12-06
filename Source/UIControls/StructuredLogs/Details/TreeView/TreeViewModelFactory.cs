using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace JsonFlier.UIControls.LogTabs.JsonLog.JsonTreeView
{
    public static class TreeViewModelFactory
    {
        public static TreeElementViewModel CreateFromJson(JToken jObject)
        {
            if (jObject == null)
                return null;

            var viewModel = CreateRecursive("DATA", jObject);
            viewModel.HeaderHidden = true;

            if (viewModel.Value != null && viewModel.MultiLineValue == null)
            {
                viewModel.MultiLineValue = viewModel.Value;
                viewModel.Value = null;
                viewModel.CollapseDisabled = false;
                viewModel.IsCollapsed = false;
            }

            return viewModel;
        }

        public static TreeElementViewModel CreateRecursive(string name, JToken jToken)
        {
            if (jToken is JArray arr)
            {
                return new TreeElementViewModel()
                {
                    Name = name,
                    Elements = arr.Select((token, i) => CreateRecursive($"[{i}]", token)).ToArray(),
                };
            }
            else if (jToken is JObject obj)
            {
                return new TreeElementViewModel()
                {
                    Name = name,
                    Elements = obj.Properties().Select(prop => CreateRecursive($"{prop.Name}", prop.Value)).ToArray(),
                };
            }
            else if (jToken is JValue val)
            {
                bool multiline = val.ToString().Length > 250;

                return new TreeElementViewModel()
                {
                    Name = name,
                    CollapseDisabled = !multiline,
                    Value = multiline ? null : val.ToString(),
                    MultiLineValue = multiline ? val.ToString() : null,
                    IsCollapsed = !multiline,
                };
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
