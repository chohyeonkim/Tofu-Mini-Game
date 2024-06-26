using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int position {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public int rotationIndex {get; private set;}

    public float stepDelay = 1f;
    public int level = 1;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float lockTime;

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.data = data;
        this.position = position;
        this.rotationIndex = 0;
        this.stepTime = Time.time + (this.stepDelay/level);
        this.lockTime = 0f;

        if (this.cells == null) {
            this.cells = new Vector3Int[this.data.cells.Length];
        }

        for (int i = 0; i < this.data.cells.Length; i++) {
            this.cells[i] = (Vector3Int)this.data.cells[i];
        }
    }

    public void Start()
    {

    }

    public void Update() 
    {
        this.board.Clear(this);

        this.lockTime += Time.deltaTime;

        // float centerX = 0;

        // if (Input.touchCount > 0) {
        //     Touch touch = Input.GetTouch(0);

        //     if (touch.position.x > centerX) {
        //         Move(Vector2Int.right);
        //     }
        //     if (touch.position.x < centerX) {
        //         Move(Vector2Int.left);
        //     }
        // }

        if (Input.GetKeyDown(KeyCode.A)) {
            Move(Vector2Int.left);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            Move(Vector2Int.right);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Move(Vector2Int.down);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            // hard down
            while (Move(Vector2Int.down)) {
                continue;
            }
            Lock();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            Rotate(-1);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            Rotate(1); 
        }

        if (Time.time >= this.stepTime) {
            Step();
        }

        this.board.Set(this);
    }

    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;

        Move(Vector2Int.down);

        if (this.lockTime >= this.lockDelay) {
            Lock();
        }
    }

    private void Lock() 
    {
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
    }

    private bool Move(Vector2Int translation) 
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = board.IsValidPosition(this, newPosition);

        if (valid) {
            this.position = newPosition;
            this.lockTime = 0f;
        }

        return valid;
    }

    private void Rotate(int direction)
    {
        int originalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);
        

        if (!TestWallKicks(this.rotationIndex, direction)) {
            // revert
            this.rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex,rotationDirection);
        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++) {
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];
            if (Move(translation)) {
                return true;
            }
        }

        return false;
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < this.cells.Length; i++) {
            Vector3 cell = this.cells[i];
            int x, y;

            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }
    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex*2;
        if (rotationDirection < 0) {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min) {
            return max-(min-input)%(max-min);
        } else {
            return min + (input-min)%(max-min);
        }
    }
}
