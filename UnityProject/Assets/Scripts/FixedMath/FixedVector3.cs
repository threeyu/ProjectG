using UnityEngine;

namespace Ogopogo.FixedMath {
    public class FixedVector3 {
        public static FixedVector3 one = new FixedVector3(Fixed64.one, Fixed64.one, Fixed64.one);
        public static FixedVector3 zero = new FixedVector3(Fixed64.zero, Fixed64.zero, Fixed64.zero);

        public Fixed64 x => mX;
        public Fixed64 y => mY;
        public Fixed64 z => mZ;

        private Fixed64 mX;
        private Fixed64 mY;
        private Fixed64 mZ;

        public FixedVector3(Fixed64 x, Fixed64 y, Fixed64 z) {
            mX = x;
            mY = y;
            mZ = z;
        }
        
        public FixedVector3(int x, int y, int z) {
            mX = Fixed64.ParseFrom(x);
            mY = Fixed64.ParseFrom(y);
            mZ = Fixed64.ParseFrom(z);
        }
        
        public FixedVector3(float x, float y, float z) {
            mX = Fixed64.ParseFrom(x);
            mY = Fixed64.ParseFrom(y);
            mZ = Fixed64.ParseFrom(z);
        }

        public static FixedVector3 operator +(FixedVector3 a, float b) {
            var fb = Fixed64.ParseFrom(b);
            return new FixedVector3(a.x + fb, a.y + fb, a.z + fb);
        }

        public static FixedVector3 operator +(FixedVector3 a, long b) {
            return new FixedVector3(a.x + b, a.y + b, a.z + b);
        }

        public static FixedVector3 operator +(FixedVector3 a, int b) {
            return new FixedVector3(a.x + b, a.y + b, a.z + b);
        }

        public static FixedVector3 operator -(FixedVector3 a, float b) {
            var fb = Fixed64.ParseFrom(b);
            return new FixedVector3(a.x + fb, a.y + fb, a.z + fb);
        }

        public static FixedVector3 operator -(FixedVector3 a, long b) {
            return new FixedVector3(a.x - b, a.y - b, a.z - b);
        }
        
        public static FixedVector3 operator -(FixedVector3 a, int b) {
            return new FixedVector3(a.x - b, a.y - b, a.z - b);
        }

        public static FixedVector3 operator *(FixedVector3 a, float b) {
            return new FixedVector3(a.x * b, a.y * b, a.z * b);
        }

        public static FixedVector3 Dot(FixedVector3 a, FixedVector3 b) {
            return new FixedVector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public FixedVector3 Dot(FixedVector3 b) {
            return Dot(this, b);
        }

        public Vector3 ToVector3() {
            return new Vector3(
                mX.ToFloat(),
                mY.ToFloat(),
                mZ.ToFloat()
            );
        }

        public static FixedVector3 ParseFrom(Vector3 v) {
            var r = new FixedVector3(
                Fixed64.ParseFrom(v.x),
                Fixed64.ParseFrom(v.y),
                Fixed64.ParseFrom(v.z)
            );
            return r;
        }
    }
}