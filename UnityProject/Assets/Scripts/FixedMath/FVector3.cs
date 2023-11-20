using UnityEngine;

namespace Ogopogo.FixedMath {
    public class FVector3 {
        public static FVector3 one = new FVector3(FNumber.one, FNumber.one, FNumber.one);
        public static FVector3 zero = new FVector3(FNumber.zero, FNumber.zero, FNumber.zero);

        public FNumber x => mX;
        public FNumber y => mY;
        public FNumber z => mZ;

        private FNumber mX;
        private FNumber mY;
        private FNumber mZ;

        public FVector3(FNumber x, FNumber y, FNumber z) {
            mX = x;
            mY = y;
            mZ = z;
        }
        
        public FVector3(int x, int y, int z) {
            mX = FNumber.Parse(x);
            mY = FNumber.Parse(y);
            mZ = FNumber.Parse(z);
        }
        
        public FVector3(float x, float y, float z) {
            mX = FNumber.Parse(x);
            mY = FNumber.Parse(y);
            mZ = FNumber.Parse(z);
        }

        public static FVector3 operator +(FVector3 a, float b) {
            var fb = FNumber.Parse(b);
            return new FVector3(a.x + fb, a.y + fb, a.z + fb);
        }

        public static FVector3 operator +(FVector3 a, long b) {
            return new FVector3(a.x + b, a.y + b, a.z + b);
        }

        public static FVector3 operator +(FVector3 a, int b) {
            return new FVector3(a.x + b, a.y + b, a.z + b);
        }

        public static FVector3 operator -(FVector3 a, float b) {
            var fb = FNumber.Parse(b);
            return new FVector3(a.x + fb, a.y + fb, a.z + fb);
        }

        public static FVector3 operator -(FVector3 a, long b) {
            return new FVector3(a.x - b, a.y - b, a.z - b);
        }
        
        public static FVector3 operator -(FVector3 a, int b) {
            return new FVector3(a.x - b, a.y - b, a.z - b);
        }

        public static FVector3 operator *(FVector3 a, float b) {
            return new FVector3(a.x * b, a.y * b, a.z * b);
        }

        public static FVector3 Dot(FVector3 a, FVector3 b) {
            return new FVector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public FVector3 Dot(FVector3 b) {
            return Dot(this, b);
        }

        public Vector3 ToVector3() {
            return new Vector3(
                mX.ToFloat(),
                mY.ToFloat(),
                mZ.ToFloat()
            );
        }

        public static FVector3 ParseFrom(Vector3 v) {
            var r = new FVector3(
                FNumber.Parse(v.x),
                FNumber.Parse(v.y),
                FNumber.Parse(v.z)
            );
            return r;
        }
    }
}