using System;

namespace mazegen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Other ot = new Other();
			Console.WriteLine ("Hello World!");
			ot.Start ();
		}
	}






	public class Other {
		static public int x = 5;
		static public int y = 10;
		public int[,] maze = new int[2 * x + 3,2 * y + 3];
		public Vector2[] jumppads = new Vector2[x * y];
		int totaljpads;
		bool full;
		bool c1 = false;
		bool c2 = false;
		bool c3 = false;
		bool c4 = false;
		int tempx;
		int tempy;
		string s;
		System.Random r = new System.Random ();

		public void Start () {
			Console.WriteLine ("not generated");
			for (int i = 1; i <= 2 * x + 1; i++) {
				for (int j = 1; j <= 2 * y + 1; j++) {
					maze [i, j] = 0;
				}
			}

			for (int i = 1; i <= 2 * x + 1; i++) {
				maze [i, 0] = 2;
				maze [i, 2 * y + 2] = 2;
			}

			for (int i = 1; i <= 2 * y + 1; i++) {
				maze [0, i] = 2;
				maze [2 * x + 2, i] = 2;
			}
			Console.WriteLine ("generated");
			result ();
			Console.WriteLine ("printed");
			mazegenerate ();
		}

		void mazegenerate () {
			full = false;
			tempx = 2 * r.Next (1, x + 1);
			tempy = 2 * r.Next (1, y + 1);
			maze [tempx, tempy] = 1;
			int direction;
			Console.WriteLine ("started filling");
			while (!full) {
				if (c1 & c2 & c3 & c4) {
					Console.WriteLine ("checking for neighbors");
					NeighborCheck ();
					RandomNeighborjump ();
					Console.WriteLine("got one! " + tempx + " " + tempy);
				}
				direction = r.Next (1, 5);
				Console.WriteLine ("new direction " + direction);
				if (true) {
					switch (direction) {
					case 1:
						if (maze [tempx, tempy + 2] < 1) {
							maze [tempx, tempy + 1] = 1;
							maze [tempx, tempy + 2] = 1;
							tempy += 2;
							creset ();
						} else {
							c1 = true;
						}
						break;
					case 2:
						if (maze [tempx, tempy - 2] < 1) {
							maze [tempx, tempy - 1] = 1;
							maze [tempx, tempy - 2] = 1;
							tempy -= 2;
							creset ();
						} else {
							c2 = true;
						}
						break;
					case 3:
						if (maze [tempx + 2, tempy] < 1) {
							maze [tempx + 1, tempy] = 1;
							maze [tempx + 2, tempy] = 1;
							tempx += 2;
							creset ();
						} else {
							c3 = true;
						}
						break;
					case 4:
						if (maze [tempx - 2, tempy] < 1) {
							maze [tempx - 1, tempy] = 1;
							maze [tempx - 2, tempy] = 1;
							tempx -= 2;
							creset ();
						} else {
							c4 = true;
						}
						break;
					}
				}
				result ();
				check ();
				Console.WriteLine (full);
			}
			result ();
		}

		void check(){
			full = true;
			for (int i = 1; i <= x; i ++) {
				for (int j = 1; j <= y; j ++) {
					if (maze [2 * i, 2 * j] == 0) {
						full = false;
						return;
					}
				}
			}
		}

		/*void restartpos(){
			tempx = 2 * r.Next (1, x + 1);
			tempy = 2 * r.Next (1, y + 1);
			if (maze [tempx, tempy] != 1) {
				restartpos ();
			}
		}*/


		void result(){
			for (int i = 0; i <= 2 * x + 2; i++) {
				string t;
				s = i.ToString();
				if (i < 10) {
					s = string.Concat("0" + s, ":   ");
				} else {
					s = string.Concat(s, ":   ");
				}

				for (int j = 0; j <= 2 * y + 2; j++) {
					if (maze [i, j] < 1) {
						t = " ";
					} else {
						t = "█";
					}

					s = string.Concat (s, t);
				}
				Console.WriteLine (s);
			}
		}

		public void NeighborCheck(){
			totaljpads = 0;
			for (int i = 2; i <= 2 * x; i += 2) {
				for (int j = 2; j <= 2 * y; j += 2) {
					if (maze [i, j] == 1) {
						if (maze [i, j + 2] < 1) {
							totaljpads++;
							jumppads[totaljpads] = new Vector2();
							jumppads [totaljpads].x = i;
							jumppads [totaljpads].y = j;
						} else if (maze [i, j - 2] < 1) {
							totaljpads++;
							jumppads[totaljpads] = new Vector2();
							jumppads [totaljpads].x = i;
							jumppads [totaljpads].y = j;
						} else if (maze [i + 2, j] < 1) {
							totaljpads++;
							jumppads[totaljpads] = new Vector2();
							jumppads [totaljpads].x = i;
							jumppads [totaljpads].y = j;
						} else if (maze [i - 2, j] < 1) {
							totaljpads++;
							jumppads[totaljpads] = new Vector2();
							jumppads [totaljpads].x = i;
							jumppads [totaljpads].y = j;
						}
					}
				}
			}
		}

		public void RandomNeighborjump() {
			int k = r.Next (1, totaljpads + 1);
			tempx = jumppads [k].x;// Math.Truncate(jumppads [k].x);
			tempy = jumppads [k].y;//Math.Truncate(jumppads [k].y);
			creset();
		}

		public void creset() {
			c1 = false;
			c2 = false;
			c3 = false;
			c4 = false;
		}

		public void checkRoots() {
			for (int i = 2; i <= 2 * x; i += 2) {
				for (int j = 2; j <= 2 * y; j += 2) {

				}
			}
		}
	}

	public class Vector2 {
		public int x;
		public int y;
	}
}
