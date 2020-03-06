using Terraria.ModLoader;

// 命名空间，注意它要与文件夹的名字相同
namespace TemplateMod2 {

    // 主要Mod类
    public class TemplateMod2 : Mod {

        // 给一个实例指针，以后会非常有用的
        private static TemplateMod2 instance;

        // 构造函数
        public TemplateMod2() {
        }

        public override void Load() {
            instance = this;
            base.Load();
        }
        public override void Unload() {
            instance = null;
            base.Unload();
        }

        public static TemplateMod2 Instance {
            get;
        }
    }
}
