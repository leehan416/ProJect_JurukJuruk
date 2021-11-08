using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    public AudioSource bgmSource = new AudioSource();
    public AudioSource rainSource = new AudioSource();
    public AudioSource fxSource = new AudioSource();

    public AudioClip[] musics = new AudioClip[5];
    public AudioClip rain; // = new AudioClip();
    public AudioClip[] fxs = new AudioClip[2];

    private void Start()
    {
        if (!instance) instance = this;
        else DestroyImmediate(this);

        // 환경 설정 변수 적용
        DataBase.getSettingVal();
        bgmSource.volume = DataBase.bgmVol;
        rainSource.volume = DataBase.fxVol;
        fxSource.volume = DataBase.fxVol;

        // 빗소리 지정
        rainSource.clip = rain;
        rainSource.Play();
    }


    // 노래 재생
    public void playMusic(int value)
    {
        bgmSource.clip = musics[value];
        fxSource.clip = fxs[0];
        bgmSource.Play();
        fxSource.Play();
    }

    // 효과음 재생
    public void playFx(int val)
    {
        fxSource.clip = fxs[val];
        fxSource.Play();
    }
}