# 井字棋部分重要代码

<p>
  <code>
  
    public int turn;
    public int count;
    private int[,] btns = new int[3, 3];
    
    private int isWin()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (btns[i, 0] != 0 && btns[i, 0] == btns[i, 1] && btns[i, 1] == btns[i, 2])
            {
                return btns[i, 0];
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (btns[0, i] != 0 && btns[0, i] == btns[1, i] && btns[1, i] == btns[2, i])
            {
                return btns[0, i];
            }
        }
        if (btns[1, 1] != 0 && btns[0, 0] == btns[1, 1] && btns[1, 1] == btns[2, 2] || btns[0, 2] == btns[1, 1] && btns[1, 1] == btns[2, 0])
        {
            return btns[1, 1];
        }
        if (count == 9) return 3;
        return 0;
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(230, 300, 240, 50), "Reset"))
        {
            Reset();
        }
        int result = isWin();
        switch (result)
        {
            case 1:
                GUI.Label(new Rect(0, 100, 100, 50), "O WIN", style: temp);
                break;
            case 2:
                GUI.Label(new Rect(0, 100, 100, 50), "X WIN", style: temp);
                break;
            case 3:
                GUI.Label(new Rect(0, 100, 100, 50), "DUAL", style: temp);
                break;
        }
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (btns[i, j] == 1)
                {
                    GUI.Button(new Rect(i * 80 + 230, j * 80 + 50, 80, 80), "O");
                }
                if (btns[i, j] == 2)
                {
                    GUI.Button(new Rect(i * 80 + 230, j * 80 + 50, 80, 80), "X");
                }
                if (GUI.Button(new Rect(i * 80 + 230, j * 80 + 50, 80, 80), ""))
                {
                    if (result == 0)
                    {
                        if (turn == 1) btns[i, j] = 1;
                        else btns[i, j] = 2;
                        count++;
                        turn = -turn;
                    }
                }
            }
        }
    }
  

  </code>
</p>
