
/*********************************
*                                *
*        Старикова Алина         *
*     "перегрузка операций"      *
*                                *
*********************************/

using System;

namespace LLABA3 {
    public class NotInvertible : Exception {
        public NotInvertible() : base("матрицу нельзя обратить!") {
        }
    }

    class Operations {
        static void Main(string[] args) {
            var Random = new Random();
            var FirstMatrix = new ImMatrix(5);
            for(int RowIndex = 0; RowIndex < 5; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < 5; ++ColumnIndex) {
                    FirstMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"первая матрица: \n{FirstMatrix}");

            var SecondMatrix = new ImMatrix(5);
            for(int RowIndex = 0; RowIndex < 5; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < 5; ++ColumnIndex) {
                    SecondMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"вторая матрица: \n{SecondMatrix}");

            Console.WriteLine($"сложение: \n{FirstMatrix + SecondMatrix}");
            Console.WriteLine($"вычитание: \n{FirstMatrix - SecondMatrix}");
            Console.WriteLine($"произведение: \n{FirstMatrix * SecondMatrix}");

            Console.WriteLine($"матрица А > матрица Б: {FirstMatrix > SecondMatrix}");
            Console.WriteLine($"матрица А >= матрица Б: {FirstMatrix >= SecondMatrix}");
            Console.WriteLine($"матрица А <= матрица Б: {FirstMatrix <= SecondMatrix}");
            Console.WriteLine($"матрица А < матрица Б: {FirstMatrix < SecondMatrix}");
            Console.WriteLine($"матрица А == матрица Б: {FirstMatrix == SecondMatrix}");
            Console.WriteLine($"матрица А != матрица Б: {FirstMatrix != SecondMatrix} \n");

            Console.WriteLine($"детерминант матрицы А: {FirstMatrix.Determinant()}");

            try {
                var InverseA = FirstMatrix.Inverse();
                  Console.WriteLine($"обратная матрица А:\n{InverseA}");
            }
            catch(NotInvertible ex) {
                  Console.WriteLine(ex.Message);
            }
        }
    }
}


    public class ImMatrix {
        private double[,] Matrix;
        public int Dimension { get; }
        public ImMatrix(int dimension) {
            Dimension = dimension;
            Matrix = new double[Dimension, Dimension];
        }

        public ImMatrix(int dimension, double MinValue, double MaxValue) {
            Dimension = dimension;
            Matrix = new double[Dimension, Dimension];
            var Random = new Random();
            for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex) {
                    Matrix[RowIndex, ColumnIndex] = Random.NextDouble() * (MaxValue - MinValue) + MinValue;
                }
            }
        }
        public double this[int RowIndex, int ColumnIndex] {
            get { return Matrix[RowIndex, ColumnIndex]; }
            set { Matrix[RowIndex, ColumnIndex] = value; }
        }

        public static ImMatrix operator + (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            if(FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("матрицы не одинакового размера!");
            var Result = new ImMatrix(FirstMatrix.Dimension);

            for(int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex) {
                    Result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex] + SecondMatrix[RowIndex, ColumnIndex];
                }
            }
            return Result;
        }

        public static ImMatrix operator - (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            var Result = new ImMatrix(FirstMatrix.Dimension);
            for(int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex) {
                    Result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex] - SecondMatrix[RowIndex, ColumnIndex];
                }
            }
            return Result;
        }
        public static ImMatrix operator * (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            var Result = new ImMatrix(FirstMatrix.Dimension);
            for(int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex) {
                    double Sum = 0;
                    for(int Index = 0; Index < Result.Dimension; ++Index) {
                        Sum += FirstMatrix[RowIndex, Index] * SecondMatrix[Index, ColumnIndex];
                    }
                    Result[RowIndex, ColumnIndex] = Sum;
                }
            }
            return Result;
        }

        public static bool operator > (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            for(int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex) {
                    if(FirstMatrix[RowIndex, ColumnIndex] <= SecondMatrix[RowIndex, ColumnIndex]){
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator < (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            for(int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex) {
                    if(FirstMatrix[RowIndex, ColumnIndex] >= SecondMatrix[RowIndex, ColumnIndex]) {
                        return false;
                    }
                    else  {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator >= (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            for(int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex) {
                for(int RowCounter = 0; RowCounter < FirstMatrix.Dimension; ++RowCounter) {
                    if(FirstMatrix[RowIndex, RowCounter] < SecondMatrix[RowIndex, RowCounter]) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator <= (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            for(int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex) {
                    if(FirstMatrix[RowIndex, ColumnIndex] > SecondMatrix[RowIndex, ColumnIndex]) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator == (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            if(FirstMatrix is null) {
                return SecondMatrix is null;
            }
            if(SecondMatrix is null || FirstMatrix.Dimension != SecondMatrix.Dimension) {
                return false;
            }
            for(int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex) {
                for(int CloumnIndex = 0; CloumnIndex < FirstMatrix.Dimension; ++CloumnIndex) {
                    if(FirstMatrix[RowIndex, CloumnIndex] != SecondMatrix[RowIndex, CloumnIndex]) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator != (ImMatrix FirstMatrix, ImMatrix SecondMatrix) {
            return ! (FirstMatrix == SecondMatrix);
        }
        public static explicit operator bool(ImMatrix Matrix) {
            return Matrix != null && Matrix.Dimension > 0;
        }

        public double Determinant() {
            if(Dimension == 1) {
                return Matrix[0, 0];
            }
            else if(Dimension == 2) {
                return Matrix[0, 0] * Matrix[1, 1] - Matrix[0, 1] * Matrix[1, 0];
            }
            else {
                double Result = 0;
                int Sign = 1;
                for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                    var subMatrix = SubMatrix(RowIndex, 0);
                    Result += Sign * Matrix[RowIndex, 0] * subMatrix.Determinant();
                    Sign = -Sign;
                }
                return Result;
            }
        }

        public ImMatrix Inverse() {
            var determinant = Determinant();
            if(determinant == 0) {
                throw new InvalidOperationException("эта матрица не может быть обратной!");
            }
            var Result = new ImMatrix(Dimension);
            int Sign = 1;
            for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex) {
                    var subMatrix = SubMatrix(RowIndex, ColumnIndex);
                    Result[ColumnIndex, RowIndex] = Sign * subMatrix.Determinant() / determinant;
                    Sign = -Sign;
                }
            }
            return Result;
        }

        private ImMatrix SubMatrix(int RowToRemove, int columnToRemove) {
            var subMatrix = new ImMatrix(Dimension - 1);
            int SubRow = 0;
            for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                if(RowIndex == RowToRemove)
                    continue;
                int SubColumn = 0;
                for(int Column = 0; Column < Dimension; ++Column) {
                    if(Column == columnToRemove)
                        continue;
                    subMatrix[SubRow, SubColumn] = Matrix[RowIndex, Column];
                    SubColumn++;
                }
                SubRow++;
            }
            return subMatrix;
        }

        public override string ToString() {
            string Result = "";
            for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex) {
                    Result += $"{Matrix[RowIndex, ColumnIndex]} ";
                }
                Result += "\n";
            }
            return Result;
        }

        public int CompareTo(ImMatrix other) {
            if(other is null)
                return 1;
            if(Dimension != other.Dimension)
                return Dimension.CompareTo(other.Dimension);
            for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex) {
                    int Compare = Matrix[RowIndex, ColumnIndex].CompareTo(other.Matrix[RowIndex, ColumnIndex]);
                    if(Compare != 0)
                        return Compare;
                }
            }
            return 0;
        }

        public override bool Equals(object Obj) {
            if(Obj is null || ! (Obj is ImMatrix)) {
                return false;
            }

            return this == (ImMatrix)Obj;
        }

        public override int GetHashCode() {
            return Matrix.GetHashCode();
        }

        public ImMatrix Clone() {
            var Clone = new ImMatrix(Dimension);
            for(int RowIndex = 0; RowIndex < Dimension; ++RowIndex) {
                for(int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex) {
                    Clone[RowIndex, ColumnIndex] = Matrix[RowIndex, ColumnIndex];
                }
            }
            return Clone;
        }
    }
