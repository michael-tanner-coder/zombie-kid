using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private IntVariable _score;
    [SerializeField] private IntVariable _scoreMultiplier;
    [SerializeField] private IntVariable _maxScoreMultiplier;
    [SerializeField] private FloatVariable _shakeAmount;
    [SerializeField] private GameObject _pointTextPrefab;
    [SerializeField] private SoundEffect _scoreSound;
    [SerializeField] private int _perfectStreakPoints;
    private bool _perfectStreak = true;

    void Awake()
    {
        _score.Value = 0;
        _scoreMultiplier.Value = 1;
    }

    public void BreakCombo()
    {
        // spawn text object to show combo break
        if (_scoreMultiplier.Value > 1)
        {
            GameObject combobreakText = Instantiate(_pointTextPrefab, transform.position, transform.rotation);
            combobreakText.GetComponent<TMP_Text>().text = "<color=#ac3232>x0</color>";
            combobreakText.transform.SetParent(transform.parent);
        }

        // reset combo multiplier
        _scoreMultiplier.Value = 1;

        // remove the player's chance to get a perfect streak combo for this round
        _perfectStreak = false;
    }

    public void IncreaseScore(int points, string label)
    {
        // spawn text object to show score increase
        GameObject pointText = Instantiate(_pointTextPrefab, transform.position, transform.rotation);
        pointText.GetComponent<TMP_Text>().text = label + "+" + points + "x" + _scoreMultiplier.Value;
        pointText.transform.SetParent(transform.parent);

        // increase the score multiplier for consecutive kills; don't increase it past the max multiplier
        _score.Value += points * _scoreMultiplier.Value;
        _scoreMultiplier.Value += 1;
        _scoreMultiplier.Value = Mathf.Clamp(_scoreMultiplier.Value, 0, _maxScoreMultiplier.Value);

        // shake screen after receiving points
        _shakeAmount.Value = 1f;
    }

    public void IncreaseScoreByBasePointValue(IntVariable points)
    {
        IncreaseScore(points.Value, "");
        ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("Score");
    }

    public void ExtraPoints()
    {
        // if the player never hit a combo break, grant them extra bonus points
        if (_perfectStreak)
        {
            IncreaseScore(_perfectStreakPoints, "<color=#fbf236>Perfect Streak:</color> ");
            ServiceLocator.Instance.Get<AudioManager>().PlaySoundFromDictionary("BonusPoints");
        }

        // reset the perfect streak tracker for the next combat round
        _perfectStreak = true;
    }
}
