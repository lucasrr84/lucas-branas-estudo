using System;

namespace ride.src.domain.vo;

public interface IRideStatus
{
    public string value { get; set; }
    void request();
    void accept();
    void start();
    void finish();
}
