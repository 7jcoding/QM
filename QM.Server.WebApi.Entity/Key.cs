namespace QM.Server.WebApi.Entity {
    public class Key {

        public string Name { get; set; }

        public string Group { get; set; }

        public static implicit operator string(Key key) {
            return string.Format("{0}:{1}", key.Name, key.Group);
        }

        public static implicit operator Key(string str) {
            var tmp = str.Split(':');
            return new Key() {
                Name = tmp[0],
                Group = tmp.Length > 1 ? tmp[1] : null
            };
        }
    }
}
