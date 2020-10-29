using System;
using System.Collections.Generic;
using GameEngine;

namespace LittleWitch
{
    public class MenuItemChoiceMods : MenuItemPrefix {
        private TextRenderer labelName;
        private TextRenderer labelVersion;

        private TextRenderer leftArrow;
        private TextRenderer rightArrow;

        public ModInfo Choice {
            get;
            private set;
        }

        private int choiceIndex;

        private List<ModInfo> Mods;

        public event Action OnChange;

        public MenuItemChoiceMods(string prefix, Vector pos, List<ModInfo> mods)
            : base(prefix, pos)
        {
            Mods = mods;
            
            labelName = AddComponent(new TextRenderer(Game.MedFont, "", TextAlign.Left, MenuItem.Purple, float.MaxValue, 4f, 0f));
            labelName.Shadow = ColorF.Black;
            labelName.Tags = GameTags.UI;

            labelVersion = AddComponent(new TextRenderer(Game.MedFont, "", TextAlign.Left, MenuItem.Purple, float.MaxValue, 4f, 0f));
            labelVersion.Shadow = ColorF.Black;
            labelVersion.Tags = GameTags.UI;

            leftArrow = AddComponent(new TextRenderer(Game.MedFont, "", TextAlign.Left, 0x847cdfff, float.MaxValue, 4f, 0f));
            leftArrow.Shadow = ColorF.Black;
            leftArrow.Tags = GameTags.UI;

            rightArrow = AddComponent(new TextRenderer(Game.MedFont, "", TextAlign.Left, 0x847cdfff, float.MaxValue, 4f, 0f));
            rightArrow.Shadow = ColorF.Black;
            rightArrow.Tags = GameTags.UI;

            var _this = this;
            OnLeft += delegate {
                MenuFx.Back.Play();
                _this.SetChoice(_this.choiceIndex > 0 ? _this.choiceIndex - 1 : mods.Count);
            };
            OnRight += delegate {
                MenuFx.Accept.Play();
                _this.SetChoice(_this.choiceIndex < mods.Count ? _this.choiceIndex + 1 : 0);
            };
        }

        private void SetChoice (int index, ModInfo modInfo) {
            Choice = modInfo;
            choiceIndex = index;

            Update();
            this.OnChange?.Invoke();
        }

        public void SetChoice(int choice) {
            SetChoice(choice, choice == 0 ? null : Mods[choice - 1]);
        }

        public void SetChoice(string id) {
            var index = Mods.FindIndex(mi => mi.ID.Equals(id));
            SetChoice(index + 1);
        }


        public override void Update() {
            var offset = 4f;

            leftArrow.Offset.X = offset;
            leftArrow.Text = Selected ? "<" : "";
            offset += Selected ? (Game.MedFont.GetWidth(leftArrow.Text) + 4f) : 0f;

            labelName.Offset.X = offset;
            labelName.Text = (Choice == null) ? "" : Choice.Name;
            labelName.Color = Selected ? MenuItem.Yellow : MenuItem.Pink1;
            offset += (labelName.Text.Length > 0) ? (Game.MedFont.GetWidth(labelName.Text) + 4f) : 0f;

            labelVersion.Offset.X = offset;
            labelVersion.Text = (Choice == null) ? "NONE" : ("v" + Choice.Version);
            labelVersion.Color = Selected ? MenuItem.Pink1 : MenuItem.Purple;
            offset += (labelVersion.Text.Length > 0) ? (Game.MedFont.GetWidth(labelVersion.Text) + 4f) : 0f;

            rightArrow.Offset.X = offset;
            rightArrow.Text = Selected ? ">" : "";
        }
    }
}
